﻿using Falcon.CommandsExecutors.Executors;
using Falcon.Protocol.Frame;

namespace Falcon.CommandsExecutors
{
    class CommandExecutorFactory
    {
        public ICommandExecutor Create(FrameType type)
        {
            switch(type)
            {
                case (FrameType.Message): return new MessageExecutor();
                case (FrameType.Ping): return new PingExecutor();
                case (FrameType.Pong): return new PongExecutor();
                case (FrameType.Disconnect): return new DisconnectExecutor();
            }

            return null;
        }
    }
}
