using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketDataReceivedEventArgs : EventArgs
    {
        public string ClientID { get; }
        public byte[] Data { get; }

        public WebSocketDataReceivedEventArgs(string clientID, byte[] data)
        {
            ClientID = clientID;
            Data = data;
        }
    }
}
