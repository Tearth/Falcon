using System;
using System.Runtime.Serialization;

namespace Falcon.Exceptions
{
    [Serializable]
    public class ServerAlreadyWorkingException : Exception
    {
        public ServerAlreadyWorkingException()
        {

        }

        public ServerAlreadyWorkingException(string message) : base(message)
        {

        }

        public ServerAlreadyWorkingException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected ServerAlreadyWorkingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
