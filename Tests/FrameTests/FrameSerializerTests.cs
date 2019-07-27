using System.Linq;
using System.Text;
using Falcon.Protocol.Frame;
using Xunit;

namespace Tests.FrameTests
{
    public class FrameSerializerTests
    {
        [Fact]
        public void GetBytes_ValidMessageFrame_ValidResult()
        {
            var serializer = new FrameSerializer();

            var exampleDataBytes = Encoding.UTF8.GetBytes("Foo Bar");
            var frame = new WebSocketFrame(exampleDataBytes, false)
            {
                Fin = true,
                OpCode = (byte)FrameType.Message
            };

            var serializedFrame = serializer.GetBytes(frame);

            var correctResult = new byte[] { 129, 7, 70, 111, 111, 32, 66, 97, 114 };
            Assert.True(serializedFrame.SequenceEqual(correctResult));
        }

        [Fact]
        public void GetBytes_PartialEmptyMessageFrame_ValidResult()
        {
            var serializer = new FrameSerializer();

            var exampleDataBytes = Encoding.UTF8.GetBytes("");
            var frame = new WebSocketFrame(exampleDataBytes, false)
            {
                Fin = false,
                OpCode = (byte)FrameType.Message
            };

            var serializedFrame = serializer.GetBytes(frame);

            var correctResult = new byte[] { 1, 0 };
            Assert.True(serializedFrame.SequenceEqual(correctResult));
        }

        [Fact]
        public void GetBytes_ValidDisconnectFrame_ValidResult()
        {
            var serializer = new FrameSerializer();

            var exampleDataBytes = Encoding.UTF8.GetBytes("Foo Bar");
            var frame = new WebSocketFrame(exampleDataBytes, false)
            {
                Fin = true,
                OpCode = (byte)FrameType.Disconnect
            };

            var serializedFrame = serializer.GetBytes(frame);

            var correctResult = new byte[] { 136, 7, 70, 111, 111, 32, 66, 97, 114 };
            Assert.True(serializedFrame.SequenceEqual(correctResult));
        }

        [Fact]
        public void GetBytes_ValidPingFrame_ValidResult()
        {
            var serializer = new FrameSerializer();

            var exampleDataBytes = Encoding.UTF8.GetBytes("Foo Bar");
            var frame = new WebSocketFrame(exampleDataBytes, false)
            {
                Fin = true,
                OpCode = (byte)FrameType.Ping
            };

            var serializedFrame = serializer.GetBytes(frame);

            var correctResult = new byte[] { 137, 7, 70, 111, 111, 32, 66, 97, 114 };
            Assert.True(serializedFrame.SequenceEqual(correctResult));
        }

        [Fact]
        public void GetBytes_ValidPongFrame_ValidResult()
        {
            var serializer = new FrameSerializer();

            var exampleDataBytes = Encoding.UTF8.GetBytes("Foo Bar");
            var frame = new WebSocketFrame(exampleDataBytes, false)
            {
                Fin = true,
                OpCode = (byte)FrameType.Pong
            };

            var serializedFrame = serializer.GetBytes(frame);

            var correctResult = new byte[] { 138, 7, 70, 111, 111, 32, 66, 97, 114 };
            Assert.True(serializedFrame.SequenceEqual(correctResult));
        }
    }
}
