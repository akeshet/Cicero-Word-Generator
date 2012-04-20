using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Database
{
    public class RunLogDatabaseException : Exception
    {
        public RunLogDatabaseException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public RunLogDatabaseException(string message) : this(message, null)
        {

        }

        private RunLogDatabaseException() : this("Unspecified exception.") {

        }

    }
}
