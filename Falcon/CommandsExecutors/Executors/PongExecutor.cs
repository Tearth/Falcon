namespace Falcon.CommandsExecutors.Executors
{
    class PongExecutor : ICommandExecutor
    {
        public bool Do(WebSocketServer webSocketServer, string clientID, byte[] message)
        {
            return false;
        }
    }
}
