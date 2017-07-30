using Falcon.SocketServices.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Falcon
{
    internal interface IServerListener
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
