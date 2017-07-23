using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketDataSentEventArgs : EventArgs
    {
        public string ClientID { get; private set; }
        public int SentBytes { get; private set; }

        public WebSocketDataSentEventArgs(string clientID, int sentBytes)
        {
            this.ClientID = clientID;
            this.SentBytes = sentBytes;
        }
    }
}
