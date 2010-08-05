using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    /// <summary>
    /// An empty class just for use as an interface to be used by serversettings object. 
    /// </summary>
    /// 
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class ServerSettingsInterface
    {
    }
}
