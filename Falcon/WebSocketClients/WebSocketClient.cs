using System;
using System.Linq;

namespace Falcon.WebSocketClients
{
    class WebSocketClient
    {
        public string ID { get; private set; }
        public bool HandshakeDone { get; set; }

        byte[] _buffer;
        int _bufferPointer;

        public WebSocketClient(string id, int bufferSize)
        {
            ID = id;
            _buffer = new byte[bufferSize];

            _bufferPointer = 0;
        }

        public bool AddToBuffer(byte[] data)
        {
            if (_bufferPointer + data.Length > _buffer.Length)
                return false;

            Array.Copy(data, 0, _buffer, _bufferPointer, data.Length);
            _bufferPointer += data.Length;
            return true;
        }

        public void RemoveFromBuffer(int length)
        {
            Array.Copy(_buffer, length, _buffer, 0, _buffer.Length - length);
            _bufferPointer -= length;
        }

        public void ClearBuffer()
        {
            Array.Clear(_buffer, 0, _buffer.Length);
            _bufferPointer = 0;
        }

        public byte[] GetBufferData()
        {
            return _buffer.Take(_bufferPointer).ToArray();
        }
    }
}
