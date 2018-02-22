namespace Falcon.CommandsExecutors.Executors
{
    public interface ICommandExecutor
    {
        bool Do(WebSocketServer webSocketServer, string clientID, byte[] message);
    }
}
