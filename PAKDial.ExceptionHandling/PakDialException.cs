using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.ExceptionHandling
{


    public sealed class PakDialException : ApplicationException
    {
        public PakDialException(string message) : base(message)
        {
        }
        public PakDialException(string message, Exception innerException)
            : base(message, innerException)
        {
        }


    }
}
