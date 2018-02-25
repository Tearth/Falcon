using System;
using System.Linq;
using System.Net.Sockets;
using Falcon.SocketServices.Clients;
using Falcon.SocketServices.EventArguments;

namespace Falcon.SocketServices
{
    /// <summary>
    /// Represents a set of methods to manage data received from clients.
    /// </summary>
    public class ReceivingDataService
    {
        /// <summary>
        /// The event triggered when new data has been received.
        /// </summary>
        public event EventHandler<DataReceivedEventArgs> ReceivedData;

        /// <summary>
        /// The event triggered when a client has disconnected.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Begins receiving data with the specified buffer size.
        /// </summary>
        /// <param name="socket">The socket to listening.</param>
        /// <param name="bufferSize">The buffer size.</param>
        public void ReceiveData(Socket socket, uint bufferSize)
        {
            ReceiveData(new Client(socket, bufferSize));
        }

        /// <summary>
        /// Begins receiving data with the specified buffer size.
        /// </summary>
        /// <param name="client">The client to listening.</param>
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
