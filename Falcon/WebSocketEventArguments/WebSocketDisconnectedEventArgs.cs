using System;

namespace Falcon.WebSocketEventArguments
{
    /// <summary>
    /// Represents a WebSocketDisconnected event arguments.
    /// </summary>
    public class WebSocketDisconnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the client ID which has disconnected.
        /// </summary>
        public string ClientID { get; }

        /// <summary>
        /// Gets the flag indicates whether the disconnection was unexpected (with some exception) or not.
        /// </summary>
        public bool Unexpected => Exception != null;

        /// <summary>
        /// Gets the exception (only if <see cref="Unexpected"/> is true).
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketDisconnectedEventArgs"/> class.
        /// </summary>
        /// <param name="clientID">The client ID which has disconnected.</param>
        public WebSocketDisconnectedEventArgs(string clientID)
        {
            ClientID = clientID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketDisconnectedEventArgs"/> class.
        /// </summary>
        /// <param name="clientID">The client ID which has disconnected.</param>
        /// <param name="exception">The exception which has been thrown.</param>
        public WebSocketDisconnectedEventArgs(string clientID, Exception exception) : this(clientID)
        {
            Exception = exception;
        }
    }
}
