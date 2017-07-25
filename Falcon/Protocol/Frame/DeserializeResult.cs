namespace Falcon.Protocol.Frame
{
    internal enum DeserializeResult
    {
        None,
        InvalidHeader,
        PartialMessage,
        SuccessWithFIN,
        SuccessWithoutFIN
    }
}
