using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    /// <summary>
    /// Represents a DataSent event arguments.
    /// </summary>
    public class DataSentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the socket which will receive sent data.
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// Gets the sent bytes count.
        /// </summary>
        public int BytesSent { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSentEventArgs"/> class.
        /// </summary>
        /// <param name="socket">The socket which will receive sent data.</param>
        /// <param name="bytesSent">The sent bytes count.</param>
        public DataSentEventArgs(Socket socket, int bytesSent)
        {
            Socket = socket;
            BytesSent = bytesSent;
        }
    }
}
