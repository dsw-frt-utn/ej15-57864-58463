using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Domain
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message) 
        { 
        }
    }
}
