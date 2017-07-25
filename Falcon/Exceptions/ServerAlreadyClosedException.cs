using System;
using System.Runtime.Serialization;

namespace Falcon.Exceptions
{
    [Serializable]
    public class ServerAlreadyClosedException : Exception
    {
        public ServerAlreadyClosedException()
        {

        }

        public ServerAlreadyClosedException(string message) : base(message)
        {

        }

        public ServerAlreadyClosedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected ServerAlreadyClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
