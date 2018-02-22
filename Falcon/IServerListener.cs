using System;
using System.Net;
using System.Net.Sockets;
using Falcon.SocketServices.EventArguments;

namespace Falcon
{
    public interface IServerListener
    {
        event EventHandler<ConnectedEventArgs> ClientConnected;
        event EventHandler<DataReceivedEventArgs> DataReceived;
        event EventHandler<DataSentEventArgs> DataSent;
        event EventHandler<DisconnectedEventArgs> ClientDisconnected;

        EServerState ServerState { get; }

        void Start(IPEndPoint endpoint);
        void Stop();
        void Send(Socket socket, byte[] data);
        void CloseConnection(Socket socket);
        void CloseConnection(Socket socket, Exception ex);

    }
}
