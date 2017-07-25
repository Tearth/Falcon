using Falcon.SocketServices.EventArguments;
using System;
using System.Net.Sockets;

namespace Falcon.SocketServices
{
    internal class ConnectingService
    {
        public event EventHandler<ConnectedEventArgs> Connected;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public void BeginConnection(Socket server)
        {
            try
            {
                server.BeginAccept(new AsyncCallback(AcceptNewConnection), server);
            }
            catch (SocketException ex)
            {
                Disconnected(this, new DisconnectedEventArgs(null, ex));
            }
        }

        void AcceptNewConnection(IAsyncResult ar)
        {
            var server = (Socket)ar.AsyncState;
            Socket clientSocket = null;

            try
            {
                clientSocket = server.EndAccept(ar);
                Connected(this, new ConnectedEventArgs(clientSocket));
            }
            catch (SocketException ex)
            {
                Disconnected(this, new DisconnectedEventArgs(null, ex));
            }
        }
    }
}
