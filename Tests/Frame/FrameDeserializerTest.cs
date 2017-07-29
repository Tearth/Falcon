using Falcon.Protocol.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Frame
{
    public class FrameDeserializerTest
    {
        [Fact]
        public void GetFrame_FullMessage_ValidMessage()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 129, 134, 167, 225, 225, 210, 198, 131, 130, 182, 194, 135 };
            var result = DeserializeResult. None;
            var frame = deserializer.GetFrame(data, out result);

            var message = ASCIIEncoding.UTF8.GetString(frame.GetMessage());
            Assert.Equal("abcdef", message);
        }

        [Fact]
        public void GetFrame_FullMessageWithFin_DeserializeResultWithFin()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 129, 134, 167, 225, 225, 210, 198, 131, 130, 182, 194, 135 };
            var result = DeserializeResult.None;
            var frame = deserializer.GetFrame(data, out result);
            
            Assert.Equal(result, DeserializeResult.SuccessWithFIN);
        }

        [Fact]
        public void GetFrame_MessageWithoutFin_DeserializeResultWithoutFin()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 1, 134, 167, 225, 225, 210, 198, 131, 130, 182, 194, 135 };
            var result = DeserializeResult.None;
            var frame = deserializer.GetFrame(data, out result);
            
            Assert.Equal(result, DeserializeResult.SuccessWithoutFIN);
        }

        [Fact]
        public void GetFrame_PartialMessage_DeserializeResultPartial()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 1, 134, 167, 225, 225, 210, 198, 131, 130 };

            var result = DeserializeResult.None;
            var frame = deserializer.GetFrame(data, out result);
            
            Assert.Equal(result, DeserializeResult.PartialMessage);
        }

        [Fact]
        public void GetFrame_PartialMessage_DeserializeResultInvalidHeader()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 1, 134, 167, 225, 225 };
            var result = DeserializeResult.None;
            var frame = deserializer.GetFrame(data, out result);
            
            Assert.Equal(result, DeserializeResult.InvalidHeader);
        }

        [Fact]
        public void GetFrame_PartialMessage2_DeserializeResultInvalidHeader()
        {
            var deserializer = new FrameDeserializer();

            var data = new byte[] { 1 };
            var result = DeserializeResult.None;
            var frame = deserializer.GetFrame(data, out result);

            Assert.Equal(result, DeserializeResult.InvalidHeader);
        }
    }
}
