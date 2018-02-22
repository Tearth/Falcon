using Falcon.CommandsExecutors.Executors;
using Falcon.Protocol.Frame;

namespace Falcon.CommandsExecutors
{
    public interface ICommandExecutorFactory
    {
        ICommandExecutor Create(FrameType type);
    }
}
