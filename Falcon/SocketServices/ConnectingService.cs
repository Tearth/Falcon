using Falcon.SocketClients;
using Falcon.SocketServices.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices
{
    class ConnectingService
    {
        public event EventHandler<ConnectedEventArgs> Connected;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public ConnectingService()
        {

        }

        public void BeginConnection(Socket server)
        {
            try
            {
                server.BeginAccept(new AsyncCallback(AcceptNewConnection), server);
            }
            catch(Exception ex)
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
            }
            catch(Exception ex)
            {
                Disconnected(this, new DisconnectedEventArgs(null, ex));
                return;
            }

            Connected(this, new ConnectedEventArgs(clientSocket));
        }
    }
}
