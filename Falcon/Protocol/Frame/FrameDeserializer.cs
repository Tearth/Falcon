using System;
using System.Linq;

namespace Falcon.Protocol.Frame
{
    /// <summary>
    /// Represents a set of methods to deserialize frames.
    /// </summary>
    public class FrameDeserializer
    {
        /// <summary>
        /// Deserializes frame from the specified bytes array.
        /// </summary>
        /// <param name="data">The bytes array to deserialize.</param>
        /// <param name="result">The deserialization result.</param>
        /// <returns>The frame object if deserialization has been done without errors, otherwise null.</returns>
        public WebSocketFrame GetFrame(byte[] data, out DeserializationResult result)
        {
            var frame = new WebSocketFrame(true);
            if (data.Length < 2)
            {
                result = DeserializationResult.InvalidHeader;
                return null;
            }

            frame.Fin = Convert.ToBoolean(data[0] >> 7);
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
                result = DeserializationResult.InvalidHeader;
                return null;
            }

            if (frame.HeaderLength + frame.PayloadLength > (ulong)data.Length)
            {
                result = DeserializationResult.PartialMessage;
                return null;
            }

            frame.Payload = data.Skip(frame.HeaderLength).ToArray();

            result = frame.Fin ? DeserializationResult.SuccessWithFin : DeserializationResult.SuccessWithoutFin;
            return frame;
        }
    }
}
