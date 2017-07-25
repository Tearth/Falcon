using System;
using System.Runtime.Serialization;

namespace Falcon.Exceptions
{
    [Serializable]
    public class BufferOverflowException : Exception
    {
        public BufferOverflowException()
        {

        }

        public BufferOverflowException(string message) : base(message)
        {

        }

        public BufferOverflowException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected BufferOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
