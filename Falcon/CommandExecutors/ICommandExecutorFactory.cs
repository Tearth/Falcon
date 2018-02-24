using Falcon.CommandExecutors.Executors;
using Falcon.Protocol.Frame;

namespace Falcon.CommandExecutors
{
    public interface ICommandExecutorFactory
    {
        ICommandExecutor Create(FrameType type);
    }
}
