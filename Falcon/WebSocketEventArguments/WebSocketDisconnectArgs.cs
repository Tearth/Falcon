using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketEventArguments
{
    class WebSocketDisconnectArgs : EventArgs
    {
        public String ClientID { get; private set; }
        public bool Unexpected { get; private set; }

        public WebSocketDisconnectArgs(String clientID, bool unexpected)
        {
            this.ClientID = clientID;
            this.Unexpected = unexpected;
        }
    }
}
