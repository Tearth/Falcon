using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketConnectedEventArgs : EventArgs
    {
        public String ClientID { get; private set; }

        public WebSocketConnectedEventArgs(String clientID)
        {
            this.ClientID = clientID;
        }
    }
}
