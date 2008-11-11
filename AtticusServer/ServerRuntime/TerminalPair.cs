using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AtticusServer
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class TerminalPair
    {
        private string sourceTerminal;

        [Description("Source terminal for the connection.")]
        public string SourceTerminal
        {
            get { return sourceTerminal; }
            set { sourceTerminal = value; }
        }
        private string destinationTerminal;

        [Description("Destination terminal for the connection.")]
        public string DestinationTerminal
        {
            get { return destinationTerminal; }
            set { destinationTerminal = value; }
        }

        public TerminalPair()
        {
            sourceTerminal = "";
            destinationTerminal = "";
        }
    }
}
