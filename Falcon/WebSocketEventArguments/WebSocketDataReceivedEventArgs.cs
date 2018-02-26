using System;

namespace Falcon.WebSocketEventArguments
{
    /// <summary>
    /// Represents a WebSocketDataReceived event arguments.
    /// </summary>
    public class WebSocketDataReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the client ID (GUID).
        /// </summary>
        public string ClientID { get; }

        /// <summary>
        /// Gets the data received from the client.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketDataReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="clientID">The client ID which sent data.</param>
        /// <param name="data">The received data.</param>
        public WebSocketDataReceivedEventArgs(string clientID, byte[] data)
        {
            ClientID = clientID;
            Data = data;
        }
    }
}
