using Falcon.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketHandlers
{
    class NewConnectionHandler
    {
        public event OnClientActionHandler OnNewClientConnection;

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
            var socket = server.EndAccept(ar);

            var client = new Client();
            client.Bind(socket);

            OnNewClientConnection(this, client);
        }
    }
}
