using Falcon.SocketClients;
using Falcon.SocketServices.EventArguments;
using System;
using System.Net.Sockets;

namespace Falcon.SocketServices
{
    class ReceivingDataService
    {
        public event EventHandler<DataReceivedEventArgs> ReceivedData;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public void ReceiveData(Client client)
        {
            var clientSocket = client.Socket;

            try
            {
                clientSocket.BeginReceive(client.Buffer, 0, client.Buffer.Length, 0,
                                          new AsyncCallback(AcceptNewData), client);
            }
            catch (ObjectDisposedException) when (client.Closed)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected(this, new DisconnectedEventArgs(client, ex));
            }
        }

        void AcceptNewData(IAsyncResult ar)
        {
            var client = (Client)ar.AsyncState;
            var receivedBytes = 0;

            try
            {
                receivedBytes = client.Socket.EndReceive(ar);

                if (receivedBytes == 0)
                    Disconnected(this, new DisconnectedEventArgs(client));
                else
                    ReceivedData(this, new DataReceivedEventArgs(client, receivedBytes));
            }
            catch (ObjectDisposedException) when (client.Closed)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected(this, new DisconnectedEventArgs(client, ex));
            }
        }
    }
}
