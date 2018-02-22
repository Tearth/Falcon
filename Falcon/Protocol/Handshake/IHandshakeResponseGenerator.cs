namespace Falcon.Protocol.Handshake
{
    public interface IHandshakeResponseGenerator
    {
        byte[] GetResponse(byte[] request);
    }
}
