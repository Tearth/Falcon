using System;

namespace Falcon.WebSocketEventArguments
{
    /// <summary>
    /// Represents a WebSocketConnected event arguments.
    /// </summary>
    public class WebSocketConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the new client ID (GUID).
        /// </summary>
        public string ClientID { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketConnectedEventArgs"/> class.
        /// </summary>
        /// <param name="clientID">The new client ID (GUID).</param>
        public WebSocketConnectedEventArgs(string clientID)
        {
            ClientID = clientID;
        }
    }
}
