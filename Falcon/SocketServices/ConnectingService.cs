using Falcon.SocketServices.EventArguments;
using System;
using System.Net.Sockets;

namespace Falcon.SocketServices
{
    public class ConnectingService
    {
        public event EventHandler<ConnectedEventArgs> Connected;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

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
