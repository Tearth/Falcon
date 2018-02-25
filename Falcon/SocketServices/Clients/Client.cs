using System.Net.Sockets;

namespace Falcon.SocketServices.Clients
{
    /// <summary>
    /// Represents a connected client to the WebSocket server.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets the client socket.
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// Gets the client buffer.
        /// </summary>
        public byte[] Buffer { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="socket">The client socket.</param>
        /// <param name="bufferSize">The client buffer size.</param>
        public Client(Socket socket, uint bufferSize)
        {
            Buffer = new byte[bufferSize];
            Socket = socket;
        }
    }
}
