namespace Falcon.CommandExecutors.Executors
{
    /// <summary>
    /// Represents a set of methods to execute when a client has started a disconnect sequence.
    /// </summary>
    public class DisconnectExecutor : ICommandExecutor
    {
        /// <summary>
        /// Disconnects a client with the specified id and removes from the internal clients list.
        /// </summary>
        /// <param name="webSocketServer">The WebSocket server.</param>
        /// <param name="clientId">The client id.</param>
        /// <param name="message">The message.</param>
        /// <returns>True if message should be propagated to the WebSocketDataReceived event, otherwise false.</returns>
        public bool Do(WebSocketServer webSocketServer, string clientId, byte[] message)
        {
            webSocketServer.DisconnectClient(clientId);

            return false;
        }
    }
}
