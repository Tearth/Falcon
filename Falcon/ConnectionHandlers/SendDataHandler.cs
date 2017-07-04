using Falcon.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.ConnectionHandlers
{
    class SendDataHandler
    {
        public event OnClientActionHandler OnDataSent;

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
            client.Socket.EndSend(ar);

            OnDataSent(this, client);
        }
    }
}
