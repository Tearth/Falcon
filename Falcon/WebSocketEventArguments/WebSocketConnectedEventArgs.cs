using System;

namespace Falcon.WebSocketEventArguments
{
    public class WebSocketConnectedEventArgs : EventArgs
    {
        public string ClientID { get; private set; }

        public WebSocketConnectedEventArgs(string clientID)
        {
            this.ClientID = clientID;
        }
    }
}
