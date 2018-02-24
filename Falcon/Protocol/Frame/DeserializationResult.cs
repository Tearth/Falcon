using System.Diagnostics.CodeAnalysis;

namespace Falcon.Protocol.Frame
{
    /// <summary>
    /// Represents a deserialization results.
    /// </summary>
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public enum DeserializationResult
    {
        None,
        InvalidHeader,
        PartialMessage,
        SuccessWithFIN,
        SuccessWithoutFIN
    }
}
