using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    class FrameDecryptor
    {
        public FrameDecryptor()
        {

        }

        public WebSocketFrame Decrypt(byte[] data, out DecryptResult result)
        {
            var frame = new WebSocketFrame();
            if(data.Length < 2)
            {
                result = DecryptResult.InvalidHeader;
                return null;
            }

            frame.FIN = Convert.ToBoolean(data[0] >> 7);
            frame.OpCode = Convert.ToByte(data[0] & 15);
            frame.Mask = Convert.ToBoolean(data[1] >> 7);
            frame.Length = Convert.ToByte(data[1] & 127);

            if(frame.Length == 126 && data.Length >= 8)
            {
                frame.Length = (ulong)((data[2] << 8) + data[3]);
                frame.MaskingKey = data.Skip(4).Take(4).ToArray();
                frame.HeaderLength = 8;
            }
            else if(frame.Length == 127 && data.Length >= 14)
            {
                frame.Length = BitConverter.ToUInt64(data.Skip(2).Take(8).ToArray(), 0);
                frame.MaskingKey = data.Skip(10).Take(4).ToArray();
                frame.HeaderLength = 14;
            }
            else if(data.Length >= 6)
            {
                frame.MaskingKey = data.Skip(2).Take(4).ToArray();
                frame.HeaderLength = 6;
            }
            else
            {
                result = DecryptResult.InvalidHeader;
                return null;
            }

            if (frame.Length + frame.HeaderLength > (ulong)data.Length)
            {
                result = DecryptResult.PartialMessage;
                return null;
            }

            frame.Payload = data.Skip(frame.HeaderLength).ToArray();

            result = frame.FIN ? DecryptResult.SuccessWithFIN : DecryptResult.SuccessWithoutFIN;
            return frame;
        }
    }
}
