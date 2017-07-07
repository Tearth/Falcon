using Falcon.SocketClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketClients
{
    class WebSocketClient
    {
        public String ID { get; private set; }
        public byte[] Buffer { get; set; }
        public bool HandshakeDone { get; set; }

        public int BufferPointer { get; private set; }

        public WebSocketClient(String id, int bufferSize)
        {
            this.ID = id;
            this.Buffer = new byte[bufferSize];

            BufferPointer = 0;
        }

        public bool AddToBuffer(byte[] data)
        {
            if (BufferPointer + data.Length > Buffer.Length)
                return false;

            Array.Copy(data, 0, Buffer, BufferPointer, data.Length);
            BufferPointer += data.Length;
            return true;
        }

        public void RemoveFromBuffer(int length)
        {
            Array.Copy(Buffer, length, Buffer, 0, Buffer.Length - length);
            BufferPointer -= length;
        }

        public void ClearBuffer()
        {
            Array.Clear(Buffer, 0, Buffer.Length);
            BufferPointer = 0;
        }
    }
}
