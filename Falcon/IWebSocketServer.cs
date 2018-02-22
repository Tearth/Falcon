using Falcon.Protocol.Frame;
using Falcon.WebSocketClients;
using Falcon.WebSocketEventArguments;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Falcon
{
    public interface IWebSocketServer
    {
        uint BufferSize { get; }
        EServerState ServerState { get; }

        event EventHandler<WebSocketConnectedEventArgs> WebSocketConnected;
        event EventHandler<WebSocketDataReceivedEventArgs> WebSocketDataReceived;
        event EventHandler<WebSocketDataSentEventArgs> WebSocketDataSent;
        event EventHandler<WebSocketDisconnectedEventArgs> WebSocketDisconnected;

        void Start(IPAddress address, ushort port);
        void Stop();

        bool SendData(string clientID, byte[] data);
        bool SendData(string clientID, string text);
        bool SendData(string clientID, byte[] data, FrameType type);
        void SendRawData(string clientID, byte[] data);

        ClientInfo GetClientInfo(string clientID);
        void DisconnectClient(string clientID);
    }
}
