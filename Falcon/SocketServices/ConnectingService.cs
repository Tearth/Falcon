using System;
using System.Net.Sockets;
using Falcon.SocketServices.EventArguments;

namespace Falcon.SocketServices
{
    /// <summary>
    /// Represents a set of methods to manage new clients.
    /// </summary>
    public class ConnectingService
    {
        /// <summary>
        /// The event triggered when a new client has connected.
        /// </summary>
        public event EventHandler<ConnectedEventArgs> Connected;

        /// <summary>
        /// The event triggered when a client has disconnected due to exception.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Begins listening for new clients and accepts them if there is no any exception.
        /// </summary>
        /// <param name="server">The server socket.</param>
        public void BeginConnection(Socket server)
        {
            try
            {
                server.BeginAccept(AcceptNewConnection, server);
            }
            catch (ObjectDisposedException)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs(null, ex));
            }
        }

        private void AcceptNewConnection(IAsyncResult ar)
        {
            var server = (Socket)ar.AsyncState;

            try
            {
                var clientSocket = server.EndAccept(ar);
                Connected?.Invoke(this, new ConnectedEventArgs(clientSocket));
            }
            catch (ObjectDisposedException)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs(null, ex));
            }
        }
    }
}
