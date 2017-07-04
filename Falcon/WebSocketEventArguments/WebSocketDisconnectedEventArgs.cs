using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketEventArguments
{
    class WebSocketDisconnectedEventArgs : EventArgs
    {
        public String ClientID { get; private set; }
        public bool Unexpected { get { return Exception != null; } }
        public Exception Exception { get; private set; }

        public WebSocketDisconnectedEventArgs(String clientID)
        {
            this.ClientID = clientID;
        }

        public WebSocketDisconnectedEventArgs(String clientID, Exception exception) : this(clientID)
        {
            this.Exception = exception;
        }
    }
}
