using Falcon.SocketClients;
using Falcon.SocketServices.EventArguments;
using System;

namespace Falcon.SocketServices
{
    class SendingDataService
    {
        public event EventHandler<DataSentEventArgs> SentData;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public SendingDataService()
        {

        }

        public void SendData(Client client, byte[] data)
        {
            var clientSocket = client.Socket;

            try
            {
                clientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(EndSendData), client);
            }
            catch(Exception ex)
            {
                Disconnected(this, new DisconnectedEventArgs(client, ex));
                return;
            }
        }

        void EndSendData(IAsyncResult ar)
        {
            var client = (Client)ar.AsyncState;
            var sentBytes = 0;

            try
            {
                sentBytes = client.Socket.EndSend(ar);
            }
            catch(Exception ex)
            {
                Disconnected(this, new DisconnectedEventArgs(client, ex));
                return;
            }

            SentData(this, new DataSentEventArgs(client, sentBytes));
        }
    }
}
