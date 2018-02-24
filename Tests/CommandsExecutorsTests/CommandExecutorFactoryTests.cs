using Falcon.CommandExecutors;
using Falcon.CommandExecutors.Executors;
using Falcon.Protocol.Frame;
using Xunit;

namespace Tests.CommandExecutorsTests
{
    public class CommandExecutorFactoryTests
    {
        [Fact]
        public void Create_Message_ValidExecutorInstance()
        {
            var factory = new CommandExecutorFactory();

            var executorInstance = factory.Create(FrameType.Message);

            Assert.IsType<MessageExecutor>(executorInstance);
        }

        [Fact]
        public void Create_Disconnect_ValidExecutorInstance()
        {
            var factory = new CommandExecutorFactory();

            var executorInstance = factory.Create(FrameType.Disconnect);

            Assert.IsType<DisconnectExecutor>(executorInstance);
        }

        [Fact]
        public void Create_Ping_ValidExecutorInstance()
        {
            var factory = new CommandExecutorFactory();

            var executorInstance = factory.Create(FrameType.Ping);

            Assert.IsType<PingExecutor>(executorInstance);
        }

        [Fact]
        public void Create_Pong_ValidExecutorInstance()
        {
            var factory = new CommandExecutorFactory();

            var executorInstance = factory.Create(FrameType.Pong);

            Assert.IsType<PongExecutor>(executorInstance);
        }

        [Fact]
        public void Create_None_NullResult()
        {
            var factory = new CommandExecutorFactory();

            var executorInstance = factory.Create(FrameType.None);

            Assert.Null(executorInstance);
        }
    }
}
