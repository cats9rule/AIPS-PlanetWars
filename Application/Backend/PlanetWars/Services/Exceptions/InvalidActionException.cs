using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanetWars.Services.Exceptions
{
    public class InvalidActionException : Exception
    {
        public InvalidActionException() : base() { }
        public InvalidActionException(string message) : base(message) { }
        public InvalidActionException(string message, Exception innerException) : base(message, innerException) { }
    }
}