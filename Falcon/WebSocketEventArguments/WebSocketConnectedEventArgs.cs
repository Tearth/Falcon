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
        public string ClientId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketConnectedEventArgs"/> class.
        /// </summary>
        /// <param name="clientId">The new client ID (GUID).</param>
        public WebSocketConnectedEventArgs(string clientId)
        {
            ClientId = clientId;
        }
    }
}
