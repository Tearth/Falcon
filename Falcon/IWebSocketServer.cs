using Falcon.Protocol.Frame;
using Falcon.WebSocketClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Falcon
{
    public interface IWebSocketServer
    {
        void Start(IPAddress address, int port);
        void Stop();

        bool SendData(string clientID, byte[] data);
        bool SendData(string clientID, string text);
        bool SendData(string clientID, byte[] data, FrameType type);
        void SendRawData(string clientID, byte[] data);

        ClientInfo GetClientInfo(string clientID);
        void DisconnectClient(string clientID);
    }
}
