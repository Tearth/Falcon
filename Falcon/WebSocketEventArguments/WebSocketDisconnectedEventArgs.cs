using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketDisconnectedEventArgs : EventArgs
    {
        public string ClientID { get; private set; }
        public bool Unexpected { get { return Exception != null; } }
        public Exception Exception { get; private set; }

        public WebSocketDisconnectedEventArgs(string clientID)
        {
            this.ClientID = clientID;
        }

        public WebSocketDisconnectedEventArgs(string clientID, Exception exception) : this(clientID)
        {
            this.Exception = exception;
        }
    }
}
