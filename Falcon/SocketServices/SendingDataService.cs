using System;
using System.Net.Sockets;
using Falcon.SocketServices.EventArguments;

namespace Falcon.SocketServices
{
    public class SendingDataService
    {
        public event EventHandler<DataSentEventArgs> SentData;
        public event EventHandler<DisconnectedEventArgs> Disconnected;

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
