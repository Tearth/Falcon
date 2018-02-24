using System.Diagnostics.CodeAnalysis;

namespace Falcon.Protocol.Frame
{
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
