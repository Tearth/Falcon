namespace Falcon.CommandExecutors.Executors
{
    /// <summary>
    /// Represents an interface for command executors.
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes action for the specified command type.
        /// </summary>
        /// <param name="webSocketServer">The WebSocket server.</param>
        /// <param name="clientID">The client id.</param>
        /// <param name="message">The message.</param>
        /// <returns>True if message should be propagated to the WebSocketDataReceived event, otherwise false.</returns>
        bool Do(WebSocketServer webSocketServer, string clientID, byte[] message);
    }
}
