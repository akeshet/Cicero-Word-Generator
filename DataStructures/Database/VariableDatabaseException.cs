using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Database
{
    public class VariableDatabaseException : Exception
    {
        public VariableDatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public VariableDatabaseException(string message)
            : this(message, null)
        {

        }

        private VariableDatabaseException()
            : this("Unspecified exception.")
        {

        }

    }
}
