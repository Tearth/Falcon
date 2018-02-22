using Falcon.SocketServices.EventArguments;
using System;
using System.Net.Sockets;

namespace Falcon.SocketServices
{
    internal class SendingDataService
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
                Disconnected(this, new DisconnectedEventArgs(socket, ex));
            }
        }

        void EndSendData(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            var sentBytes = 0;

            try
            {
                sentBytes = socket.EndSend(ar);
                SentData(this, new DataSentEventArgs(socket, sentBytes));
            }
            catch (ObjectDisposedException)
            {
                //Do nothing, socket is already closed by WebSocket server
            }
            catch (SocketException ex)
            {
                Disconnected(this, new DisconnectedEventArgs(socket, ex));
            }
        }
    }
}
