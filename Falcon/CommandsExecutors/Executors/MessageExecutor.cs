namespace Falcon.CommandsExecutors.Executors
{
    internal class MessageExecutor : ICommandExecutor
    {
        public bool Do(WebSocketServer webSocketServer, string clientID, byte[] message)
        {
            return true;
        }
    }
}
