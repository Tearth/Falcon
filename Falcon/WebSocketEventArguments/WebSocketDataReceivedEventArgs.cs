using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketDataReceivedEventArgs : EventArgs
    {
        public string ClientID { get; private set; }
        public byte[] Data { get; private set; }

        public WebSocketDataReceivedEventArgs(string clientID, byte[] data)
        {
            ClientID = clientID;
            Data = data;
        }
    }
}
