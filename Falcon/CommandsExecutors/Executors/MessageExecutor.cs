﻿namespace Falcon.CommandsExecutors.Executors
{
    public class MessageExecutor : ICommandExecutor
    {
        public bool Do(WebSocketServer webSocketServer, string clientID, byte[] message)
        {
            return true;
        }
    }
}
