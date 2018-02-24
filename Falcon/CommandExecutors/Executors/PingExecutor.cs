using Falcon.Protocol.Frame;

namespace Falcon.CommandExecutors.Executors
{
    /// <summary>
    /// Represents a set of methods to execute when a client has sent ping command.
    /// </summary>
    public class PingExecutor : ICommandExecutor
    {
        /// <summary>
        /// Responds pong to the client.
        /// </summary>
        /// <param name="webSocketServer">The WebSocket server.</param>
        /// <param name="clientID">The client id.</param>
        /// <param name="message">The message.</param>
        /// <returns>True if message should be propagated to the WebSocketDataReceived event, otherwise false.</returns>
        public bool Do(WebSocketServer webSocketServer, string clientID, byte[] message)
        {
            webSocketServer.SendData(clientID, message, FrameType.Pong);
            return false;
        }
    }
}
