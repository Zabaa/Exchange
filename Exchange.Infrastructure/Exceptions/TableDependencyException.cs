using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.Exceptions
{
    public class TableDependencyException : Exception
    {
        public TableDependencyException(string message)
            : base(message)
        { }

        public TableDependencyException(string message, Exception exception)
            : base(message, exception)
        { }
    }
}
