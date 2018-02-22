using Falcon.Protocol.Handshake;
using Xunit;

namespace Tests.HandshakeTests
{
    public class HandshakeKeyGeneratorTests
    {
        [Fact]
        public void Get_ExampleKey_ValidResponse()
        {
            var keyGenerator = new HandshakeKeyGenerator();

            var result = keyGenerator.Get("x3JJHMbDL1EzLkh9GBhXDw==");

            Assert.Equal("HSmrc0sMlYUkAGmm5OPpG2HaGWk=", result);
        }

        [Fact]
        public void Get_ExampleKey2_ValidResponse()
        {
            var keyGenerator = new HandshakeKeyGenerator();

            var result = keyGenerator.Get("dGhlIHNhbXBsZSBub25jZQ==");

            Assert.Equal("s3pPLMBiTxaQ9kYGzzhZRbK+xOo=", result);
        }
    }
}
