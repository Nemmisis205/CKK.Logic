using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Exceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("Invalid ID. Please only use positive numbers.") { }
        public InvalidIdException(string message) : base(message) { }
        public InvalidIdException(string message, Exception inner) : base(message, inner) { }
    }
}
