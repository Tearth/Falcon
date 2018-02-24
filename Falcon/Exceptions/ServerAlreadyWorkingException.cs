using System;
using System.Runtime.Serialization;

namespace Falcon.Exceptions
{
    /// <summary>
    /// The exception that is thrown when server was open and some method was trying to start it again.
    /// </summary>
    [Serializable]
    public class ServerAlreadyWorkingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAlreadyWorkingException"/> class.
        /// </summary>
        public ServerAlreadyWorkingException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAlreadyWorkingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServerAlreadyWorkingException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAlreadyWorkingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ServerAlreadyWorkingException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAlreadyWorkingException"/> class.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected ServerAlreadyWorkingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
