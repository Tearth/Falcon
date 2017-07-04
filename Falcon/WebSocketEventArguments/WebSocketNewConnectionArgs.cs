using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.WebSocketEventArguments
{
    class WebSocketNewConnectionArgs : EventArgs
    {
        public String ClientID { get; private set; }

        public WebSocketNewConnectionArgs(String clientID)
        {
            this.ClientID = clientID;
        }
    }
}
