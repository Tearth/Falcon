using Falcon.CommandsExecutors.Executors;
using Falcon.Protocol.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.CommandsExecutors
{
    public interface ICommandExecutorFactory
    {
        ICommandExecutor Create(FrameType type);
    }
}
