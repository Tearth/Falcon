using Falcon.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketHandlers
{
    class ReceiveDataHandler
    {
        public event OnClientActionHandler OnNewDataReceived;

        public ReceiveDataHandler()
        {

        }

        public void ReceiveData(Client client)
        {
            var clientSocket = client.Socket;
            clientSocket.BeginReceive(client.Buffer, 0, client.BufferSize, 0, 
                                      new AsyncCallback(AcceptNewData), client);
        }

        void AcceptNewData(IAsyncResult ar)
        {
            var client = (Client)ar.AsyncState;
            client.Socket.EndReceive(ar);

            OnNewDataReceived(this, client);
        }
    }
}
