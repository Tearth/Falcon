using Falcon.Clients;
using Falcon.SocketServices.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices
{
    class SendDataHandler
    {
        public event EventHandler<SentDataArgs> OnDataSent;
        public event EventHandler<DisconnectArgs> OnDisconnect;

        public SendDataHandler()
        {

        }

        public void SendData(Client client, byte[] data)
        {
            var clientSocket = client.Socket;

            try
            {
                clientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(EndSendData), client);
            }
            catch
            {
                OnDisconnect(this, new DisconnectArgs(client, true));
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
            catch
            {
                OnDisconnect(this, new DisconnectArgs(client, true));
                return;
            }

            var sentDataArgs = new SentDataArgs(client, sentBytes);
            OnDataSent(this, sentDataArgs);
        }
    }
}
