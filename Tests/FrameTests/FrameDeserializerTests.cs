using System.Text;
using Falcon.Protocol.Frame;
using Xunit;

namespace Tests.FrameTests
{
    public class FrameDeserializerTests
    {
        [Fact]
        public void GetFrame_FullMessage_ValidMessage()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 129, 134, 167, 225, 225, 210, 198, 131, 130, 182, 194, 135 };
            var result = DeserializationResult. None;
            var frame = deserializer.GetFrame(data, out result);

            var message = Encoding.UTF8.GetString(frame.GetMessage());
            Assert.Equal("abcdef", message);
        }

        [Fact]
        public void GetFrame_FullMessageWithFin_DeserializeResultWithFin()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 129, 134, 167, 225, 225, 210, 198, 131, 130, 182, 194, 135 };
            deserializer.GetFrame(data, out var result);

            Assert.Equal(result, DeserializationResult.SuccessWithFIN);
        }

        [Fact]
        public void GetFrame_MessageWithoutFin_DeserializeResultWithoutFin()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 1, 134, 167, 225, 225, 210, 198, 131, 130, 182, 194, 135 };
            deserializer.GetFrame(data, out var result);

            Assert.Equal(result, DeserializationResult.SuccessWithoutFIN);
        }

        [Fact]
        public void GetFrame_PartialMessage_DeserializeResultPartial()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 1, 134, 167, 225, 225, 210, 198, 131, 130 };
            deserializer.GetFrame(data, out var result);

            Assert.Equal(result, DeserializationResult.PartialMessage);
        }

        [Fact]
        public void GetFrame_PartialMessage_DeserializeResultInvalidHeader()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 1, 134, 167, 225, 225 };
            deserializer.GetFrame(data, out var result);

            Assert.Equal(result, DeserializationResult.InvalidHeader);
        }

        [Fact]
        public void GetFrame_PartialMessage2_DeserializeResultInvalidHeader()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 1 };
            deserializer.GetFrame(data, out var result);

            Assert.Equal(result, DeserializationResult.InvalidHeader);
        }
    }
}
