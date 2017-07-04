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
            clientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(EndSendData), client);
        }

        void EndSendData(IAsyncResult ar)
        {
            var client = (Client)ar.AsyncState;
            var sentBytes = client.Socket.EndSend(ar);

            var sentDataArgs = new SentDataArgs(client, sentBytes);
            OnDataSent(this, sentDataArgs);
        }
    }
}
