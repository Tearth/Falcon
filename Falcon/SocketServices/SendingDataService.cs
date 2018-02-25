using System;
using System.Net.Sockets;
using Falcon.SocketServices.EventArguments;

namespace Falcon.SocketServices
{
    /// <summary>
    /// Represents a set of methods to send data to clients.
    /// </summary>
    public class SendingDataService
    {
        /// <summary>
        /// The event triggered when a data has been sent to the client.
        /// </summary>
        public event EventHandler<DataSentEventArgs> SentData;

        /// <summary>
        /// The event triggered when a client has disconnected due to exception.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Begins sending data to the specified socket.
        /// </summary>
        /// <param name="socket">The socket which will receive the data.</param>
        /// <param name="data">The data to send.</param>
        public void SendData(Socket socket, byte[] data)
        {
            try
            {
                socket.BeginSend(data, 0, data.Length, 0, EndSendData, socket);
            }
            catch (ObjectDisposedException)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs(socket, ex));
            }
        }

        private void EndSendData(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;

            try
            {
                var sentBytes = socket.EndSend(ar);
                SentData?.Invoke(this, new DataSentEventArgs(socket, sentBytes));
            }
            catch (ObjectDisposedException)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs(socket, ex));
            }
        }
    }
}
