using System;

namespace Falcon.WebSocketEventArguments
{
    /// <summary>
    /// Represents a WebSocketDataSent event arguments.
    /// </summary>
    public class WebSocketDataSentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the client ID (GUID).
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Gets the sent bytes count.
        /// </summary>
        public int SentBytes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketDataSentEventArgs"/> class.
        /// </summary>
        /// <param name="clientId">The client ID which will receive sent data.</param>
        /// <param name="sentBytes">The sent bytes count.</param>
        public WebSocketDataSentEventArgs(string clientId, int sentBytes)
        {
            ClientId = clientId;
            SentBytes = sentBytes;
        }
    }
}
