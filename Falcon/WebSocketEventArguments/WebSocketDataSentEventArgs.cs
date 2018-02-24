using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketDataSentEventArgs : EventArgs
    {
        public string ClientID { get; }
        public int SentBytes { get; }

        public WebSocketDataSentEventArgs(string clientID, int sentBytes)
        {
            ClientID = clientID;
            SentBytes = sentBytes;
        }
    }
}
