using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Falcon.WebSocketClients;
using Xunit;

namespace Tests.WebSocketClientTests
{
    public class BufferTests
    {
        [Fact]
        public void Add_ValidData_TrueResult()
        {
            var buffer = new Buffer(8);

            var result = buffer.Add(new byte[] { 1, 2, 3 });

            Assert.True(result);
        }

        [Fact]
        public void Add_TooBigData_FalseResult()
        {
            var buffer = new Buffer(8);

            var result = buffer.Add(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Assert.False(result);
        }

        [Fact]
        public void Get_ValidData_ValidLength()
        {
            var buffer = new Buffer(8);

            var result = buffer.Add(new byte[] { 1, 2, 3 });
            buffer.Remove(2);

            var data = buffer.GetData();
            Assert.True(data.Length == 1);
        }

        [Fact]
        public void Get_ValidData_ValidResult()
        {
            var buffer = new Buffer(8);

            var result = buffer.Add(new byte[] { 1, 2, 3 });
            buffer.Remove(2);

            var data = buffer.GetData();
            Assert.True(data[0] == 3);
        }

        [Fact]
        public void Clear_ValidData_EmptyBuffer()
        {
            var buffer = new Buffer(8);

            var result = buffer.Add(new byte[] { 1, 2, 3 });
            buffer.Clear();
            
            Assert.True(buffer.GetData().Length == 0);
        }
    }
}
