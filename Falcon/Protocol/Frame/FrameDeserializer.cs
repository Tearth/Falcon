using System;
using System.Linq;

namespace Falcon.Protocol.Frame
{
    internal class FrameDeserializer
    {
        public WebSocketFrame GetFrame(byte[] data, out DeserializeResult result)
        {
            var frame = new WebSocketFrame(true);
            if (data.Length < 2)
            {
                result = DeserializeResult.InvalidHeader;
                return null;
            }

            frame.FIN = Convert.ToBoolean(data[0] >> 7);
            frame.OpCode = Convert.ToByte(data[0] & 15);
            frame.PayloadLengthSignature = Convert.ToByte(data[1] & 127);

            if (frame.PayloadLengthSignature == 126 && data.Length >= 8)
            {
                frame.PayloadExtendedLength = (ulong)((data[2] << 8) + data[3]);
                frame.MaskingKey = data.Skip(4).Take(4).ToArray();
            }
            else if (frame.PayloadLengthSignature == 127 && data.Length >= 14)
            {
                frame.PayloadExtendedLength = BitConverter.ToUInt64(data.Skip(2).Take(8).Reverse().ToArray(), 0);
                frame.MaskingKey = data.Skip(10).Take(4).ToArray();
            }
            else if (data.Length >= 6)
            {
                frame.MaskingKey = data.Skip(2).Take(4).ToArray();
            }
            else
            {
                result = DeserializeResult.InvalidHeader;
                return null;
            }

            if (frame.HeaderLength + frame.PayloadLength > (ulong)data.Length)
            {
                result = DeserializeResult.PartialMessage;
                return null;
            }

            frame.Payload = data.Skip(frame.HeaderLength).ToArray();

            result = frame.FIN ? DeserializeResult.SuccessWithFIN : DeserializeResult.SuccessWithoutFIN;
            return frame;
        }
    }
}
