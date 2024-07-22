using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.ExceptionHandling
{
    public class PakDialContentExceptions
    {
        public string Message { get; set; }
        public string ExceptionType { get { return PakDialExceptionTypes.GeneralException; } }
    }
}
