using System;
using System.Net;
using System.Net.Sockets;

namespace Falcon.WebSocketClients
{
    /// <summary>
    /// Represents a WebSocket client.
    /// </summary>
    public class WebSocketClient
    {
        /// <summary>
        /// Gets the client ID (GUID).
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the client socket.
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// Gets or sets the flag indicates whether the handshake has been done or not.
        /// </summary>
        public bool HandshakeDone { get; set; }

        /// <summary>
        /// Gets the client buffer.
        /// </summary>
        public Buffer Buffer { get; }

        /// <summary>
        /// Gets the client join time.
        /// </summary>
        public DateTime JoinTime { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketClient"/> class.
        /// </summary>
        public WebSocketClient(Socket socket, uint bufferSize)
        {
            Id = Guid.NewGuid().ToString();
            Socket = socket;
            Buffer = new Buffer(bufferSize);
            JoinTime = DateTime.Now;
        }

        /// <summary>
        /// Gets the client information.
        /// </summary>
        /// <returns>The client information.</returns>
        public ClientInfo GetInfo()
        {
            var remoteEndpoint = (IPEndPoint)Socket.RemoteEndPoint;
            return new ClientInfo
            {
                ClientId = Id,
                Ip = remoteEndpoint.Address,
                Port = remoteEndpoint.Port,
                JoinTime = JoinTime
            };
        }
    }
}
