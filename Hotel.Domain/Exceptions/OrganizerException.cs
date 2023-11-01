using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    internal class OrganizerException : Exception
    {
        public OrganizerException(string? message) : base(message)
        {
        }

        public OrganizerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
