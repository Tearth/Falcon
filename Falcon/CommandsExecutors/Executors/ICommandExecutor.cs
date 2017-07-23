namespace Falcon.CommandsExecutors.Executors
{
    interface ICommandExecutor
    {
        bool Do(WebSocketServer webSocketServer, string clientID, byte[] message);
    }
}
