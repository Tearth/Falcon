using Falcon.SocketClients;
using Falcon.SocketServices.EventArguments;
using System;
using System.Net.Sockets;

namespace Falcon.SocketServices
{
    class SendingDataService
    {
        public event EventHandler<DataSentEventArgs> SentData;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public void SendData(Client client, byte[] data)
        {
            var clientSocket = client.Socket;

            try
            {
                clientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(EndSendData), client);
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

        void EndSendData(IAsyncResult ar)
        {
            var client = (Client)ar.AsyncState;
            var sentBytes = 0;

            try
            {
                sentBytes = client.Socket.EndSend(ar);
                SentData(this, new DataSentEventArgs(client, sentBytes));
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
