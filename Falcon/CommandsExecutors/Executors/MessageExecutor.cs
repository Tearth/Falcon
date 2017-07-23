namespace Falcon.CommandsExecutors.Executors
{
    class MessageExecutor : ICommandExecutor
    {
        public bool Do(WebSocketServer webSocketServer, string clientID, byte[] message)
        {
            return true;
        }
    }
}
