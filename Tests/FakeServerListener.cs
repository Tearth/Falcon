using Falcon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Falcon.SocketServices.EventArguments;
using System.Net;
using System.Net.Sockets;

namespace Tests
{
    public class FakeServerListener : IServerListener
    {
        public EServerState ServerState { get; private set; }

        public event EventHandler<ConnectedEventArgs> ClientConnected;
        public event EventHandler<DisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<DataSentEventArgs> DataSent;

        public FakeServerListener()
        {
            ServerState = EServerState.Closed;
        }

        public void Start(IPEndPoint endpoint)
        {
            ServerState = EServerState.Working;
        }

        public void Stop()
        {
            ServerState = EServerState.Closed;
        }

        public void CloseConnection(Socket socket)
        {

        }

        public void CloseConnection(Socket socket, Exception ex)
        {

        }

        public void Send(Socket socket, byte[] data)
        {

        }
    }
}
