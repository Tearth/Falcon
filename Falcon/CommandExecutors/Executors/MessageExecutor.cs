namespace Falcon.CommandExecutors.Executors
{
    /// <summary>
    /// Represents a set of methods to execute when a new message has received.
    /// </summary>
    public class MessageExecutor : ICommandExecutor
    {
        /// <summary>
        /// Executes an action when new message has received.
        /// </summary>
        /// <param name="webSocketServer">The WebSocket server.</param>
        /// <param name="clientId">The client id.</param>
        /// <param name="message">The message.</param>
        /// <returns>True if message should be propagated to the WebSocketDataReceived event, otherwise false.</returns>
        public bool Do(WebSocketServer webSocketServer, string clientId, byte[] message)
        {
            return true;
        }
    }
}
