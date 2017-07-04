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
        public event EventHandler<DisconnectArgs> OnDisconnect;

        public ReceiveDataHandler()
        {

        }

        public void ReceiveData(Client client)
        {
            var clientSocket = client.Socket;

            try
            {
                clientSocket.BeginReceive(client.Buffer, 0, client.BufferSize, 0,
                                          new AsyncCallback(AcceptNewData), client);
            }
            catch
            {
                OnDisconnect(this, new DisconnectArgs(client, true));
                return;
            }
        }

        void AcceptNewData(IAsyncResult ar)
        {
            var client = (Client)ar.AsyncState;
            var receivedBytes = 0;

            try
            {
                receivedBytes = client.Socket.EndReceive(ar);
            }
            catch
            {
                OnDisconnect(this, new DisconnectArgs(client, true));
                return;
            }

            if (receivedBytes == 0)
                OnDisconnect(this, new DisconnectArgs(client, false));
            else
                OnDataReceived(this, new ReceivedDataArgs(client, receivedBytes));
        }
    }
}
