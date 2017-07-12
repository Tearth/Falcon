using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.Protocol.Frame
{
    class FramesManager
    {
        FrameDecryptor decryptor;

        public FramesManager()
        {
            decryptor = new FrameDecryptor();
        }

        public byte[] Encrypt(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Decrypt(byte[] frame, out DecryptResult result)
        {
            return decryptor.Decrypt(frame, out result);
        }
    }
}
