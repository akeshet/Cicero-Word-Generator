#include "stdafx.h"
//#using <DataStructures.dll>
#include "SimplicioServer.h"
#include "Form1.h"
namespace forms2{
	//using namespace DataStructures;
	//using namespace System::Collections::Generic;
	using namespace System::Runtime::Remoting;
	using namespace System::Runtime::Remoting::Channels;
	using namespace System::Runtime::Remoting::Channels::Tcp;
	SimplicioServer::SimplicioServer(Form1^ f){
		mainForm=f;
		setNextTimeMainForm = gcnew DelegateTime(f,&Form1::setNextTime);
		seqStartedMainForm = gcnew DelegateVars(f,&Form1::sequenceStarted);
		connect();	
	}
	void SimplicioServer::connect(){
		//RemotingConfiguration::Configure("forms2.exe.config",false);
		try{
			TcpChannel^ tcpChannel  = gcnew TcpChannel(5678);
			ChannelServices::RegisterChannel(tcpChannel, false);
			ObjRef^ objRef  = RemotingServices::Marshal(this, "serverCommunicator"); 
		}catch(Exception^){}
	}
	bool SimplicioServer::armTasks()
	{	
		return true;
	}
	ServerCommunicator::BufferGenerationStatus SimplicioServer::generateBuffers(int listIterationNumber)
    {
		return 	ServerCommunicator::BufferGenerationStatus::Success;
    }

    bool SimplicioServer::generateTrigger()
    {
        return true;
    }
	List<HardwareChannel>^ SimplicioServer::getHardwareChannels()
    {
        return gcnew List<HardwareChannel>();
    }
	String^ SimplicioServer::getServerName()
    {
        return "Simplicio Server";
    }

	ServerSettingsInterface^ SimplicioServer::getServerSettings()
    {
        return gcnew ServerSettingsInterface();
    }

	void SimplicioServer::nextRunTimeStamp(DateTime timeStamp)
    {
		array<Object^>^ parameters = gcnew array<Object^>(1);
		parameters[0] = timeStamp;
		mainForm->BeginInvoke(setNextTimeMainForm, parameters);

    }

	bool SimplicioServer::outputGPIBGroup(GPIBGroup^ gpibGroup, SettingsData^ settings)
    {
        return true;
    }

	bool SimplicioServer::outputRS232Group(RS232Group^ rs232Group, SettingsData^ settings)
    {
        return true;
    }

    bool SimplicioServer::outputSingleTimestep(SettingsData^ settings, SingleOutputFrame^ output)
    {
        return true;
    }

    bool SimplicioServer::ping()
    {
        //messageLog(this, new MessageEvent("Received PING from client."));
        return true;
    }

    bool SimplicioServer::runSuccess()
    {
       return true;
    }

    bool SimplicioServer::setSequence(SequenceData^ sequence)
    {
		List<Variable^>^ vars = sequence->Variables;
		LinkedList<Variable^>^ listVars = gcnew LinkedList<Variable^>();
		int count(0);
		for each(Variable^ var in vars){
			if (var->ListDriven){
				Variable^ newVar = gcnew Variable();
				newVar->VariableValue = var->VariableValue;
				newVar->VariableName = var->VariableName;
				listVars->AddLast(newVar);
				count++;
			}
		}
        array<Object^>^ parameters = gcnew array<Object^>(1);
		parameters[0] = listVars;
		mainForm->BeginInvoke(seqStartedMainForm, parameters);
        return true;
    }

    bool SimplicioServer::setSettings(SettingsData^ settings)
    {
        return true;
    }

    void SimplicioServer::stop()
    {
        
    }
}