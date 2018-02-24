using Falcon.CommandExecutors.Executors;
using Falcon.Protocol.Frame;

namespace Falcon.CommandExecutors
{
    /// <summary>
    /// Represents an interface for command executor factory.
    /// </summary>
    public interface ICommandExecutorFactory
    {
        /// <summary>
        /// Creates a new command executor object based on command type.
        /// </summary>
        /// <param name="type">The command type.</param>
        /// <returns>The command executor factory or null if passed type is unrecognised.</returns>
        ICommandExecutor Create(FrameType type);
    }
}
