using System;
using System.Net;

namespace Falcon.WebSocketClients
{
    /// <summary>
    /// Represents a connected client information.
    /// </summary>
    public class ClientInfo
    {
        /// <summary>
        /// Gets or sets the client ID (GUID).
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Gets or sets the client IP.
        /// </summary>
        public IPAddress IP { get; set; }

        /// <summary>
        /// Gets or sets the client port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the client join time.
        /// </summary>
        public DateTime JoinTime { get; set; }
    }
}
