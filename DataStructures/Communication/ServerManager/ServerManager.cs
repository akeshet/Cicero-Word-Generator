using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DataStructures;
using System.Runtime;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Reflection;
using System.Threading;

namespace DataStructures
{
    /// <summary>
    /// Stores information about what servers to connect to. Also used to communicate with servers in a multi-threaded
    /// way so as not to block UI threads, and so as to catch and handle .NET remoting exceptions should they occur.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter)), Serializable]
    public class ServerManager
    {
        /// <summary>
        /// Used to ensure thread safety of servermanager.
        /// </summary>
        /// 
        [NonSerialized]
        private Object lockObj;


        [OnDeserialized]
        public void ensureProperDeserialization(StreamingContext sc)
        {
            this.createNonserializedObjects();
            this.clearConnections();             
        }

        private List<ServerInfo> servers;

        [NonSerialized]
        private List<string> connectedServerNames;

        [Description("List of servers. Add servers to this list to connect to new servers.")]
        public List<ServerInfo> Servers
        {
            get { return servers; }
        }

        [NonSerialized]
        private Dictionary<ServerInfo, ServerCommunicator> communicators;

        public ServerManager()
        {
            this.servers = new List<ServerInfo>();
            createNonserializedObjects();

        }

        private void createNonserializedObjects()
        {
            this.communicators = new Dictionary<ServerInfo, ServerCommunicator>();
            this.connections = new Dictionary<ServerInfo, ConnectionStatus>();
            lockObj = new Object();
            this.connectedServerNames = new List<string>();
        }

        public enum ConnectionStatus { Connecting, Connected, Unable_To_Connect, 
            Disconnected_Normal, Disconnected_Error, Disabled, Not_Responding,
        Error_Name_Not_Unique};

        [NonSerialized]
        private Dictionary<ServerInfo, ConnectionStatus> connections;

        [Description("A list, indexed by server name or server IP address, of the connection status of the servers.")]
        public Dictionary<ServerInfo, ConnectionStatus> ConnectionsStatuses
        {
            get { return connections; }
        }

        [Description("The number of servers that are currently successfully connected.")]
        public int ConnectedServersCount
        {
            get
            {
                int ans = 0;
                foreach (ConnectionStatus status in connections.Values)
                    if (status == ConnectionStatus.Connected)
                        ans++;
                return ans;
            }
        }

        public bool isServerConnected(ServerInfo server)
        {
            if (!connections.ContainsKey(server))
                return false;
            if (connections[server] == ConnectionStatus.Connected)
                return true;
            return false;
        }

        public bool isServerConnected(string serverName)
        {
            // look through the connected servers to see if any have the given name
            foreach (ServerInfo server in connections.Keys)
            {
                if (connections[server] == ConnectionStatus.Connected)
                    if (server.ServerName == serverName)
                        return true;
            }
            return false;
        }

        public List<HardwareChannel> getAllHardwareChannels()
        {
            List<HardwareChannel> ans = new List<HardwareChannel>();
            lock (servers)
            {
                foreach (ServerInfo server in servers)
                {
                    if (connections.ContainsKey(server) && connections[server] == ConnectionStatus.Connected)
                    {
                        try
                        {
                            List<HardwareChannel> temp = communicators[server].getHardwareChannels();
                            ans.AddRange(temp);
                        }
                        catch (Exception)
                        {
                            connections[server] = ConnectionStatus.Disconnected_Error;
                        }
                    }
                }
            }
            return ans;
        }

        public bool connectAllEnabledServer(EventHandler<MessageEvent> messageLog)
        {
            try
            {
                if (messageLog != null)
                    messageLog(this, new MessageEvent("Attempting to connecting all enabled servers."));

                bool ans = true;
                lock (servers)
                {
                    foreach (ServerInfo server in servers)
                    {
                        if (server.ServerEnabled)
                        {
                            bool temp = connectServer(server, messageLog);

                            ans = ans && temp;
                        }
                    }
                }

                if (messageLog != null)
                    messageLog(this, new MessageEvent("Finished attempt to connect all enabled servers."));
                return ans;
            }
            catch (Exception e)
            {
                messageLog(this, new MessageEvent("Caught exception when attempting to connect to servers: " + e.Message + e.StackTrace));
                return false;
            }
        }

        public bool connectAllStartupServers(EventHandler<MessageEvent> messageLog)
        {

            if (messageLog != null)
                messageLog(this, new MessageEvent("Connecting all startup servers."));

            bool ans = true;
            lock (servers)
            {
                foreach (ServerInfo server in servers)
                {
                    if (server.ServerEnabled && server.ConnectOnStartup)
                    {
                        ans = ans && connectServer(server, messageLog);
                    }
                }
            }

            if (messageLog != null)
                messageLog(this, new MessageEvent("Finished attempt to connect all startup servers."));
            return ans;
        }

        public bool clearConnections()
        {
            bool gotLock = System.Threading.Monitor.TryEnter(lockObj, 100);
            try
            {
                if (!gotLock)
                    return false;

                connections.Clear();
                communicators.Clear();
                lock (servers)
                {
                    foreach (ServerInfo server in servers)
                    {
                        connections.Add(server, ConnectionStatus.Disconnected_Normal);
                    }
                }
                connectedServerNames.Clear();
            }
            finally
            {
                if (gotLock)
                    Monitor.Exit(lockObj);
            }
            return true;
        }

        #region connectServer(...)

        public bool connectServer(ServerInfo server, EventHandler<MessageEvent> messageLog)
        {

            if (messageLog != null)
                messageLog(this, new MessageEvent("Attempting to connect server " + server.ToString()));

            bool gotLock = Monitor.TryEnter(lockObj, 1000);
            try
            {
                if (!gotLock)
                {
                    if (messageLog != null)
                        messageLog(this, new MessageEvent("Unable to obtain lock on servermanager. Aborting attempt."));
                    return false;
                }
                
                // Cannot connect to a server that isn't is the servers list.
                if (!servers.Contains(server))
                {
                    if (messageLog != null)
                        messageLog(this, new MessageEvent(server.ToString() + " is not on list of known servers. Aborting."));
                    return false;
                }

                // ensure that this server has a server connection status item
                if (!connections.ContainsKey(server))
                {
                    connections.Add(server, ConnectionStatus.Disconnected_Normal);
                }

                // if this server is disabled, then don't connect to it.
                if (!server.ServerEnabled)
                {
                    if (messageLog != null)
                        messageLog(this, new MessageEvent(server.ToString() + " is not enabled. Aborting."));
                    return false;
                }

                // set server status to connecting. If the server already was connecting, then return.
                if (connections[server] == ConnectionStatus.Connecting)
                {
                    if (messageLog != null)
                        messageLog(this, new MessageEvent(server.ToString() + " is already connecting. Aborting."));
                    return false;
                }

                connections[server] = ConnectionStatus.Connecting;


                // create an empty spot for the server communicator
                if (!communicators.ContainsKey(server))
                {
                    communicators.Add(server, null);
                }

                communicators[server] = null;


                // try to create the communicator
                try
                {

                    Thread connectThread = new Thread(new ParameterizedThreadStart(server_connect_proc));
                    connectThread.Start(server);

                    connectThread.Join(1000);
                    if (connectThread.ThreadState == ThreadState.Running)
                    {
                        connectThread.Abort();
                        if (messageLog != null)
                            messageLog(this, new MessageEvent("Connection took longer than 1000 ms, aborting."));

                        connectThread.Abort();
                        return false;
                    }


                }
                catch (Exception e)
                {
                    if (messageLog != null)
                        messageLog(this, new MessageEvent("Caught exception when attempting to connect to server " + server.ToString() + ":" + e.Message + e.StackTrace));
                    connections[server] = ConnectionStatus.Unable_To_Connect;
                    return false;
                }

                try
                {

                    if (pingServer(server, messageLog))
                    {
                        server.setServerName(communicators[server].getServerName());

                        if (connectedServerNames.Contains(server.ServerName))
                        {
                            connections[server] = ConnectionStatus.Error_Name_Not_Unique;
                            communicators.Remove(server);
                            return false;
                        }

                        connectedServerNames.Add(server.ServerName);
                        connections[server] = ConnectionStatus.Connected;
                        messageLog(this, new MessageEvent("Server connected successfully."));
                        return true;
                    }
                    else
                    {
                        if (messageLog != null)
                            messageLog(this, new MessageEvent("Server ping failed. Aborting."));
                        connections[server] = ConnectionStatus.Unable_To_Connect;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    messageLog(this, new MessageEvent("Exception when attempting to ping server: " + e.Message + e.StackTrace));
                    connections[server] = ConnectionStatus.Unable_To_Connect;
                    return false;
                }
            }
            finally
            {
                if (gotLock)
                    Monitor.Exit(lockObj);
            }
            return true;
        }

        private void server_connect_proc(object obj)
        {
            if (obj == null)
                throw new Exception("server_connect_proc was passed a null object.");

            ServerInfo server = obj as ServerInfo;
            if (server == null)
                throw new Exception("server_connect_proc was passed an object other than a ServerInfo.");

            communicators[server] = (ServerCommunicator)Activator.GetObject(typeof(ServerCommunicator),
            "tcp://" + server.ServerAddress + ":5678/serverCommunicator");
        }

        #endregion

        public bool pingServer(ServerInfo server, EventHandler<MessageEvent> messageLog)
        {
            if (!communicators.ContainsKey(server))
            {
                if (messageLog != null)
                    messageLog(this, new MessageEvent("Attempted to ping unconnected server " + server.ToString()));
                return false;
            }

            ServerCommunicator comm = communicators[server];
            if (comm == null)
            {
                if (messageLog != null)
                    messageLog(this, new MessageEvent("Server communication object is null, unable to ping server " + server.ToString()));
                return false;
            }

            try
            {
                Thread pingThread = new Thread(new ParameterizedThreadStart(ping_server_proc));
                pingThread.Start(comm);
                pingThread.Join(1000);
                if (pingThread.ThreadState == ThreadState.Running)
                {
                    if (messageLog != null)
                        messageLog(this, new MessageEvent("Server ping took longer than 1000ms. Aborting."));
                    
                    pingThread.Abort();

                    return false;
                }
                
            }
            catch (Exception e)
            {
                if (messageLog != null)
                    messageLog(this, new MessageEvent("Caught exception when attempting to ping server " + server.ToString() + ": " + e.Message + e.StackTrace));
                return false;
            }

            return true;
        }

        private static void ping_server_proc(object obj)
        {
            ServerCommunicator comm = (ServerCommunicator)obj;
            comm.ping();
        }

        private void serverGotDisconnectedInError(ServerInfo server) {
            if (verifyConnection(server)== ServerActionStatus.Failed_No_Connection)
                return;
            communicators.Remove(server);
            connections[server] = ConnectionStatus.Disconnected_Error;
        }

        public enum ServerActionStatus { Success, Failed_No_Connection, Failed_Loss_Of_Connection, Failed_On_Server };



        /// <summary>
        /// Runs the method, specified by method, on the server specified by i, with parameters given by parameters.
        /// Correctly handles exceptions thrown in the running of that method, and bool and BufferGenerationStatus
        /// type return values from the server.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="i"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ServerActionStatus runMethodOnServer(MethodInfo method, ServerInfo i, object[] parameters, int msTimeout, EventHandler<MessageEvent> messageLog)
        {
            if (verifyConnection(i) == ServerActionStatus.Failed_No_Connection)
                return ServerActionStatus.Failed_No_Connection;

            ServerCommunicator communicator = communicators[i];

            Object returnValue;

            try
            {
                InvokeMethodOnServer_Parameters invoke_parameters = new InvokeMethodOnServer_Parameters(method, parameters, communicator);
                Thread invokeMethodThread = new Thread(new ParameterizedThreadStart(InvokeMethodOnServer_Proc));
                invokeMethodThread.Start(invoke_parameters);

               // invokeMethodThread.Join(msTimeout);
                invokeMethodThread.Join();
                
                if (invokeMethodThread.ThreadState == ThreadState.Running)
                {
                    invokeMethodThread.Abort();

                    throw new Exception("Method on server " + i.ToString() + "." + method.Name + " took longer than " + msTimeout + " ms timeout. Aborting.");
                }

                returnValue = invoke_parameters.returnValue;

                if (invoke_parameters.runException != null)
                {
                    throw new Exception("Caught an exception when attempting to run method " + i.ToString() + "." + method.Name+ ": " + invoke_parameters.runException.Message);
                }
            }
            catch (Exception e)
            {
                if (messageLog != null)
                    messageLog(this, new MessageEvent("Caught exception when attempting to run method on server " + i.ToString() + ": " + e.Message + e.StackTrace));

                serverGotDisconnectedInError(i);
                return ServerActionStatus.Failed_Loss_Of_Connection;
            }

            if (returnValue is bool)
            {
                bool attempt = (bool) returnValue;
                if (attempt)
                    return ServerActionStatus.Success;
                return ServerActionStatus.Failed_On_Server;
            }
            else if (returnValue is ServerCommunicator.BufferGenerationStatus)
            {
                ServerCommunicator.BufferGenerationStatus bufferStatus = (ServerCommunicator.BufferGenerationStatus) returnValue;
                if (bufferStatus == ServerCommunicator.BufferGenerationStatus.Success)
                    return ServerActionStatus.Success;
                return ServerActionStatus.Failed_On_Server;
            }

            return ServerActionStatus.Success;
        }

        /// <summary>
        /// This class is used to pass information to InvokeMethodOnServer_Proc (run in a separate thread)
        /// so that it can make the appropriate call to server communicator, with appropriate arguments,
        /// and can send back the return value.
        /// </summary>
        private class InvokeMethodOnServer_Parameters
        {
            public MethodInfo method;
            public object[] parameters;
            public ServerCommunicator communicator;

            public Object returnValue;

            public InvokeMethodOnServer_Parameters(MethodInfo method, object[] parameters, ServerCommunicator communicator)
            {
                this.method = method;
                this.parameters = parameters;
                this.communicator = communicator;
            }

            public Exception runException;
        }

        /// <summary>
        /// This is the function that actually makes the call to server communicator.
        /// 
        /// It is run in a separate thread, for historical reasons.
        /// </summary>
        /// <param name="obj"></param>
        private static void InvokeMethodOnServer_Proc(object obj)
        {
            InvokeMethodOnServer_Parameters parameters = (InvokeMethodOnServer_Parameters)obj;
            if (parameters != null)
            {
                try
                {
                    parameters.returnValue = parameters.method.Invoke(parameters.communicator, parameters.parameters);
                }
                catch (Exception e)
                {
                    parameters.runException = e;
                }
            }
        }

        private delegate ServerActionStatus runMethodOnServerDelegate(MethodInfo method, ServerInfo i, object[] parameters, int msTimeout, EventHandler<MessageEvent> messageLog);

        /// <summary>
        /// Runs the specified method on all of the connected servers. Runs the methods in parallel if there are multiple
        /// connected servers. Intelligently handles remoting exceptions and return values.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ServerActionStatus runMethodOnConnectedServers(MethodInfo method, object[] parameters, int msTimeout, EventHandler<MessageEvent> messageLog)
        {
            Dictionary<IAsyncResult, runMethodOnServerDelegate> delegates = new Dictionary<IAsyncResult, runMethodOnServerDelegate>();

            lock (servers)
            {
                foreach (ServerInfo server in servers)
                {
                    if (isServerConnected(server))
                    {
                        runMethodOnServerDelegate runDelegate = new runMethodOnServerDelegate(runMethodOnServer);
                        IAsyncResult result = runDelegate.BeginInvoke(method, server, parameters, msTimeout, messageLog, null, null);
                        delegates.Add(result, runDelegate);
                    }
                }
            }

            ServerActionStatus ans = ServerActionStatus.Success;
            // now wait for all the threaded operations to complete
            foreach (IAsyncResult res in delegates.Keys)
            {
                runMethodOnServerDelegate del = delegates[res];
                ServerActionStatus status = del.EndInvoke(res);
                if (status != ServerActionStatus.Success)
                {
                    ans = status;
                }
            }
            return ans;
        }


        /// <summary>
        /// Cache of servercommunicator method infos, used by runNamedMethodOnConnectedServers.
        /// Eliminates the need for constant use of type.getMethod().
        /// </summary>
        [NonSerialized]
        private Dictionary<string, MethodInfo> methodInfoCache;

        private Dictionary<string, MethodInfo> MethodInfoCache
        {
            get
            {
                if (methodInfoCache == null)
                    methodInfoCache = new Dictionary<string, MethodInfo>();
                return methodInfoCache;
            }
        }

        /// <summary>
        /// Used in runNamedMethodOnConnectedServers. Eliminates the need for constant use of typeof.
        /// </summary>
        [NonSerialized]
        private Type serverCommunicatorType;


        /// <summary>
        /// Runs the method with name methodName on all of the connected servers.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private ServerActionStatus runNamedMethodOnConnectedServers(string methodName, object[] parameters, int msTimeout, EventHandler<MessageEvent> messageLog)
        {
            

            if (serverCommunicatorType == null)
                serverCommunicatorType = typeof(ServerCommunicator);

            MethodInfo method;

            lock (MethodInfoCache)
            {
                if (MethodInfoCache.ContainsKey(methodName))
                    method = MethodInfoCache[methodName];
                else
                {
                    method = serverCommunicatorType.GetMethod(methodName);
                    if (method == null)
                        throw new Exception("Unable to find a method of ServerCommunicator with name " + methodName + ". If you have recently changed ServerCommunicator.cs, did you also remember to update ServerManager.cs?");
                    MethodInfoCache.Add(methodName, method);
                }
            }
            return runMethodOnConnectedServers(method, parameters, msTimeout, messageLog);
        }

        public ServerActionStatus setSettingsOnConnectedServers(SettingsData settings, EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("setSettings", new object[] { settings }, 4000, messageLog);
        }

        public ServerActionStatus setSequenceOnConnectedServers(SequenceData sequence, EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("setSequence", new object[] { sequence }, 4000, messageLog);
        }

        public ServerActionStatus generateBuffersOnConnectedServers(int iterationNumber, EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("generateBuffers", new object[] { iterationNumber }, 20000, messageLog);
        }

        public ServerActionStatus armTasksOnConnectedServers(UInt32 clockID, EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("armTasks", new object[] { clockID }, 4000, messageLog);
        }

        public ServerActionStatus generateTriggersOnConnectedServers(EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("generateTrigger", null, 4000, messageLog);
        }

        public ServerActionStatus getRunSuccessOnConnectedServers(EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("runSuccess", null, 4000, messageLog);
        }

        public ServerActionStatus setNextRunTimestampOnConnectedServers(DateTime timeStamp, EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("nextRunTimeStamp", new object[] { timeStamp }, 4000, messageLog);
        }

        public ServerActionStatus stopAllServers(EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("stop", null, 4000, messageLog);
        }

        public ServerActionStatus outputSingleTimestepOnConnectedServers(SettingsData settings, SingleOutputFrame output, EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("outputSingleTimestep", new object[] { settings, output }, 5000, messageLog);
        }

        public ServerActionStatus outputGPIBGroupOnConnectedServers(GPIBGroup gpibGroup, SettingsData settings, EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("outputGPIBGroup", new object[] { gpibGroup, settings }, 4000, messageLog);
        }

        public ServerActionStatus outputRS232GroupOnConnectedServers(RS232Group rs232Group, SettingsData settings, EventHandler<MessageEvent> messageLog)
        {
            return runNamedMethodOnConnectedServers("outputRS232Group", new object[] { rs232Group, settings }, 4000, messageLog);
        }

        private ServerActionStatus verifyConnection(ServerInfo server)
        {
            if (!connections.ContainsKey(server))
                return ServerActionStatus.Failed_No_Connection;
            if (connections[server] != ConnectionStatus.Connected)
                return ServerActionStatus.Failed_No_Connection;
            if (!communicators.ContainsKey(server))
                return ServerActionStatus.Failed_No_Connection;
            if (communicators[server] == null)
                return ServerActionStatus.Failed_No_Connection;
            return ServerActionStatus.Success;
        }



        /// <summary>
        /// Converts a List of strings to one string with comma separations between entries.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string convertListOfServersToOneString(List<string> names)
        {
            // turn the missing servers into a human readable list
            string ans = "";
            foreach (string str in names)
            {
                ans += str + ", ";
            }
            ans.Remove(ans.Length - 2);
            return ans;
        }

        private void thisFunctionNeverGetsCalled(ServerCommunicator temp)
        {
            // The purpose of this function is to be found by perplexed programmers
            // who have attempted to search for references to ServerCommunicator methods.

            // The reason this is necessary is that there are NO direct calls made to any ServerCommunicator
            // methods. There is an added layer of complexity which was added to allow for better performance. 

            // The added complexity is that instead of just calling methods on ServerCommunicator, I am instead 
            // calling them from the function InvokeMethodOnServer_Proc, run in its own thread. This allows me
            // to kill or cancel the call to ServerCommunicator if it times out, due for instance to a network
            // disconnection or a server shutting down.

            // As a result of this, and in order to make the InvokeMethodOnServer_Proc function more general purpose,
            // there are no direct calls to ServerCommunicator functions. Instead, they are referenced by name
            // through the .NET reflection MethodInfo class. To see how this works, look at the function 
            // setSettingsOnConnectedServers and work your way down.

            // Should you need to add new methods to ServerCommunicator, and thus corresponding new
            // methods to ServerManager, it should be straightforward to copy the technique I used.

            // Alas, this is a bit confusing and not particularly transparent, which is why I have
            // created this note.

            // -- Aviv Keshet

            temp.armTasks(0);
            temp.generateBuffers(0);
            temp.generateTrigger();
            temp.getHardwareChannels();
            temp.getServerName();
            temp.getServerSettings();
            temp.nextRunTimeStamp(DateTime.Now);
            temp.outputGPIBGroup(null, null);
            temp.outputRS232Group(null, null);
            temp.outputSingleTimestep(null, null);
            temp.ping();
            temp.runSuccess();
            temp.setSequence(null);
            temp.setSettings(null);
            temp.stop();
        }

    }
}
