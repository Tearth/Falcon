using Falcon.CommandExecutors.Executors;
using Falcon.Protocol.Frame;

namespace Falcon.CommandExecutors
{
    /// <summary>
    /// Represents a factory for command executors.
    /// </summary>
    public class CommandExecutorFactory : ICommandExecutorFactory
    {
        /// <summary>
        /// Creates a new command executor object based on command type.
        /// </summary>
        /// <param name="type">The command type.</param>
        /// <returns>The command executor factory or null if passed type is unrecognised.</returns>
        public ICommandExecutor Create(FrameType type)
        {
            switch (type)
            {
                case FrameType.Message: return new MessageExecutor();
                case FrameType.Ping: return new PingExecutor();
                case FrameType.Pong: return new PongExecutor();
                case FrameType.Disconnect: return new DisconnectExecutor();
            }

            return null;
        }
    }
}
