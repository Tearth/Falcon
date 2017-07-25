﻿using Falcon.Protocol.Frame;

namespace Falcon.CommandsExecutors.Executors
{
    internal class PingExecutor : ICommandExecutor
    {
        public bool Do(WebSocketServer webSocketServer, string clientID, byte[] message)
        {
            webSocketServer.SendData(clientID, message, FrameType.Pong);
            return false;
        }
    }
}
