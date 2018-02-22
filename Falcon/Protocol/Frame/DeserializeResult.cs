namespace Falcon.Protocol.Frame
{
    public enum DeserializeResult
    {
        None,
        InvalidHeader,
        PartialMessage,
        SuccessWithFIN,
        SuccessWithoutFIN
    }
}
