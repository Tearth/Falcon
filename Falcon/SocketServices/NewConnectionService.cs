using Falcon.Clients;
using Falcon.SocketServices.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices
{
    class NewConnectionHandler
    {
        public event EventHandler<NewConnectionArgs> OnNewClientConnection;
        public event EventHandler<DisconnectArgs> OnDisconnect;

        public NewConnectionHandler()
        {

        }

        public void BeginConnection(Socket server)
        {
            server.BeginAccept(new AsyncCallback(AcceptNewConnection), server);
        }

        void AcceptNewConnection(IAsyncResult ar)
        {
            var server = (Socket)ar.AsyncState;
            Socket clientSocket = null;

            try
            {
                clientSocket = server.EndAccept(ar);
            }
            catch
            {
                return;
            }

            var client = new Client();
            client.Bind(clientSocket);

            var connectionArgs = new NewConnectionArgs(client);
            OnNewClientConnection(this, connectionArgs);
        }
    }
}
