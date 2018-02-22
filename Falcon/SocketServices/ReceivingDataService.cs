using Falcon.SocketServices.Clients;
using Falcon.SocketServices.EventArguments;
using System;
using System.Linq;
using System.Net.Sockets;

namespace Falcon.SocketServices
{
    public class ReceivingDataService
    {
        public event EventHandler<DataReceivedEventArgs> ReceivedData;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public void ReceiveData(Socket socket)
        {
            ReceiveData(new Client(socket, 1000));
        }

        public void ReceiveData(Client client)
        {
            var clientSocket = client.Socket;

            try
            {
                clientSocket.BeginReceive(client.Buffer, 0, client.Buffer.Length, 0,
                                          AcceptNewData, client);
            }
            catch (ObjectDisposedException)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs(client.Socket, ex));
            }
        }

        private void AcceptNewData(IAsyncResult ar)
        {
            var client = (Client)ar.AsyncState;

            try
            {
                var receivedBytes = client.Socket.EndReceive(ar);

                if (receivedBytes == 0)
                {
                    Disconnected?.Invoke(this, new DisconnectedEventArgs(client.Socket));
                }
                else
                {
                    ReceivedData?.Invoke(this, new DataReceivedEventArgs(client.Socket, client.Buffer.Take(receivedBytes).ToArray()));
                }
            }
            catch (ObjectDisposedException)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs(client.Socket, ex));
            }
        }
    }
}
