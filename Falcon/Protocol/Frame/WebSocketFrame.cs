using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    class WebSocketFrame
    {
        public bool FIN { get; set; }
        public byte OpCode { get; set; }
        public bool Mask { get; set; }
        public ulong Length { get; set; }

        public byte HeaderLength { get; set; }

        public byte[] MaskingKey { get; set; }
        public byte[] Payload { get; set; }

        public byte[] GetMessage()
        {
            var message = new byte[Length];
            for (ulong i = 0; i < Length; i++)
            {
                message[i] = (byte)(Payload[i] ^ MaskingKey[i % 4]);
            }

            return message;
        }
    }
}
