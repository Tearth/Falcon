using System;
using System.Linq;

namespace Falcon.WebSocketClients
{
    class Buffer
    {
        byte[] _buffer;
        int _bufferPointer;

        public Buffer(int bufferSize)
        {
            _buffer = new byte[bufferSize];
            _bufferPointer = 0;
        }

        public bool Add(byte[] data)
        {
            if (_bufferPointer + data.Length > _buffer.Length)
                return false;

            Array.Copy(data, 0, _buffer, _bufferPointer, data.Length);
            _bufferPointer += data.Length;
            return true;
        }

        public void Remove(int length)
        {
            Array.Copy(_buffer, length, _buffer, 0, _buffer.Length - length);
            _bufferPointer -= length;
        }

        public void Clear()
        {
            Array.Clear(_buffer, 0, _buffer.Length);
            _bufferPointer = 0;
        }

        public byte[] GetData()
        {
            return _buffer.Take(_bufferPointer).ToArray();
        }
    }
}
