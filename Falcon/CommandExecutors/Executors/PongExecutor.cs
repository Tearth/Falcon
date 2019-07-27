namespace Falcon.CommandExecutors.Executors
{
    /// <summary>
    /// Represents a set of methods to execute when a client has sent pong command.
    /// </summary>
    public class PongExecutor : ICommandExecutor
    {
        /// <summary>
        /// Does nothing. For pong command, no action is necessary.
        /// </summary>
        /// <param name="webSocketServer">The WebSocket server.</param>
        /// <param name="clientId">The client id.</param>
        /// <param name="message">The message.</param>
        /// <returns>True if message should be propagated to the WebSocketDataReceived event, otherwise false.</returns>
        public bool Do(WebSocketServer webSocketServer, string clientId, byte[] message)
        {
            return false;
        }
    }
}
