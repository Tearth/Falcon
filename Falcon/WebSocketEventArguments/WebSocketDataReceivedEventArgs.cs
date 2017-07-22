using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketDataReceivedEventArgs : EventArgs
    {
        public String ClientID { get; private set; }
        public byte[] Data { get; private set; }

        public WebSocketDataReceivedEventArgs(String clientID, byte[] data)
        {
            this.ClientID = clientID;
            this.Data = data;
        }
    }
}
