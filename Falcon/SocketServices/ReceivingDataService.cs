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
    class ReceivingDataService
    {
        public event EventHandler<DataReceivedEventArgs> ReceivedData;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public ReceivingDataService()
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
            catch(Exception ex)
            {
                Disconnected(this, new DisconnectedEventArgs(client, ex));
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
            catch(Exception ex)
            {
                Disconnected(this, new DisconnectedEventArgs(client, ex));
                return;
            }

            if (receivedBytes == 0)
                Disconnected(this, new DisconnectedEventArgs(client));
            else
                ReceivedData(this, new DataReceivedEventArgs(client, receivedBytes));
        }
    }
}
