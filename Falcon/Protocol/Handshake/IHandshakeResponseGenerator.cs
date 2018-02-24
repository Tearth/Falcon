namespace Falcon.Protocol.Handshake
{
    /// <summary>
    /// Represents an interface for generate handshake response.
    /// </summary>
    public interface IHandshakeResponseGenerator
    {
        /// <summary>
        /// Generates response for the specified HTTP handshake request.
        /// </summary>
        /// <param name="request">The HTTP handshake request.</param>
        /// <returns>The array of bytes with response if request is valid, otherwise empty array.</returns>
        byte[] GetResponse(byte[] request);
    }
}
