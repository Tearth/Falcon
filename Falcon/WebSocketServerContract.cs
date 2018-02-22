using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Falcon.Protocol.Frame;
using Falcon.WebSocketClients;
using Falcon.WebSocketEventArguments;
using System.Diagnostics.Contracts;
using Falcon.Exceptions;

namespace Falcon
{
    [ContractClassFor(typeof(IWebSocketServer))]
    public abstract class WebSocketServerContract : IWebSocketServer
    {
        public uint BufferSize { get; }
        public EServerState ServerState { get; }

        public event EventHandler<WebSocketConnectedEventArgs> WebSocketConnected;
        public event EventHandler<WebSocketDataReceivedEventArgs> WebSocketDataReceived;
        public event EventHandler<WebSocketDataSentEventArgs> WebSocketDataSent;
        public event EventHandler<WebSocketDisconnectedEventArgs> WebSocketDisconnected;

        public void DisconnectClient(string clientID)
        {
            Contract.Requires<ServerAlreadyClosedException>(ServerState == EServerState.Working);
            Contract.Requires<ArgumentNullException>(clientID != null, nameof(clientID));
        }

        public ClientInfo GetClientInfo(string clientID)
        {
            Contract.Requires<ServerAlreadyClosedException>(ServerState == EServerState.Working);
            Contract.Requires<ArgumentNullException>(clientID != null, nameof(clientID));

            return default(ClientInfo);
        }

        public bool SendData(string clientID, string text)
        {
            return default(bool);
        }

        public bool SendData(string clientID, byte[] data)
        {
            return default(bool);
        }

        public bool SendData(string clientID, byte[] data, FrameType type)
        {
            Contract.Requires<ServerAlreadyClosedException>(ServerState == EServerState.Working);
            Contract.Requires<ArgumentNullException>(clientID != null, nameof(clientID));
            Contract.Requires<ArgumentNullException>(data != null, nameof(data));

            return default(bool);
        }

        public void SendRawData(string clientID, byte[] data)
        {
            Contract.Requires<ServerAlreadyClosedException>(ServerState == EServerState.Working);
            Contract.Requires<ArgumentNullException>(clientID != null, nameof(clientID));
            Contract.Requires<ArgumentNullException>(data != null, nameof(data));
        }

        public void Start(IPAddress address, ushort port)
        {
            Contract.Requires<ServerAlreadyWorkingException>(ServerState == EServerState.Closed);
            Contract.Requires<ArgumentNullException>(address != null, nameof(address));
        }

        public void Stop()
        {
            Contract.Requires<ServerAlreadyClosedException>(ServerState == EServerState.Working);
        }
    }
}
