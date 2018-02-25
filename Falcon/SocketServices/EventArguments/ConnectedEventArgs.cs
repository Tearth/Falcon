using System;
using System.Net.Sockets;

namespace Falcon.SocketServices.EventArguments
{
    /// <summary>
    /// Represents a Connected event arguments.
    /// </summary>
    public class ConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the new client socket.
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedEventArgs"/> class.
        /// </summary>
        /// <param name="socket">The new client socket.</param>
        public ConnectedEventArgs(Socket socket)
        {
            Socket = socket;
        }
    }
}
