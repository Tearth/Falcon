using System;
using System.Linq;

namespace Falcon.WebSocketClients
{
    /// <summary>
    /// Represents a client buffer which contains all received data (which can be partitioned).
    /// </summary>
    public class Buffer
    {
        private readonly byte[] _buffer;
        private int _bufferPointer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Buffer"/> class.
        /// </summary>
        /// <param name="bufferSize">The buffer size.</param>
        public Buffer(uint bufferSize)
        {
            _buffer = new byte[bufferSize];
            _bufferPointer = 0;
        }

        /// <summary>
        /// Adds an array of bytes to the buffer.
        /// </summary>
        /// <param name="data">The array of bytes to add.</param>
        /// <returns>True if data has been successfully added, otherwise false (there is no enough free space).</returns>
        public bool Add(byte[] data)
        {
            if (_bufferPointer + data.Length > _buffer.Length)
            {
                return false;
            }

            Array.Copy(data, 0, _buffer, _bufferPointer, data.Length);
            _bufferPointer += data.Length;
            return true;
        }

        /// <summary>
        /// Removes first x bytes from the buffer.
        /// </summary>
        /// <param name="length">The bytes count to remove.</param>
        public void Remove(int length)
        {
            Array.Copy(_buffer, length, _buffer, 0, _buffer.Length - length);
            _bufferPointer -= length;
        }

        /// <summary>
        /// Clears whole buffer.
        /// </summary>
        public void Clear()
        {
            Array.Clear(_buffer, 0, _buffer.Length);
            _bufferPointer = 0;
        }

        /// <summary>
        /// Gets the stored data (without unused space).
        /// </summary>
        /// <returns>The stored data.</returns>
        public byte[] GetData()
        {
            return _buffer.Take(_bufferPointer).ToArray();
        }
    }
}
