using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    /// <summary>
    /// Represents a DataReceived event arguments.
    /// </summary>
    public class DataReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the socket which sent data.
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// Gets the received data.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="socket">The socket which sent data.</param>
        /// <param name="data">The received data.</param>
        public DataReceivedEventArgs(Socket socket, byte[] data)
        {
            Socket = socket;
            Data = data;
        }
    }
}
