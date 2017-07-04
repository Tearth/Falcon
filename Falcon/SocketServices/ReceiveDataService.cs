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
    class ReceiveDataHandler
    {
        public event EventHandler<ReceivedDataArgs> OnDataReceived;

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
            var receivedBytes = client.Socket.EndReceive(ar);

            var receivedDataArgs = new ReceivedDataArgs(client, receivedBytes);
            OnDataReceived(this, receivedDataArgs);
        }
    }
}
