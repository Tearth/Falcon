using System;
using System.Runtime.Serialization;

namespace Falcon.Exceptions
{
    /// <summary>
    /// The exception that is thrown when some method has been executed with closed server.
    /// </summary>
    [Serializable]
    public class ServerAlreadyClosedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAlreadyClosedException"/> class.
        /// </summary>
        public ServerAlreadyClosedException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAlreadyClosedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServerAlreadyClosedException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAlreadyClosedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ServerAlreadyClosedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAlreadyClosedException"/> class.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected ServerAlreadyClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
