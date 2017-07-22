using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketDataSentEventArgs : EventArgs
    {
        public String ClientID { get; private set; }
        public int SentBytes { get; private set; }

        public WebSocketDataSentEventArgs(String clientID, int sentBytes)
        {
            this.ClientID = clientID;
            this.SentBytes = sentBytes;
        }
    }
}
