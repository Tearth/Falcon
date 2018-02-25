using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    /// <summary>
    /// Represents a Disconnected event arguments.
    /// </summary>
    public class DisconnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the socket which has disconnected.
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// Gets the flag indicates whether the disconnection was unexpected (with some exception) or not.
        /// </summary>
        public bool Unexpected => Exception != null;

        /// <summary>
        /// Gets the exception (only if <see cref="Unexpected"/> is true).
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisconnectedEventArgs"/> class.
        /// </summary>
        /// <param name="socket">The socket which has disconnected.</param>
        public DisconnectedEventArgs(Socket socket)
        {
            Socket = socket;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisconnectedEventArgs"/> class.
        /// </summary>
        /// <param name="socket">The socket which has disconnected.</param>
        /// <param name="exception">The exception which has been thrown.</param>
        public DisconnectedEventArgs(Socket socket, Exception exception) : this(socket)
        {
            Exception = exception;
        }
    }
}
