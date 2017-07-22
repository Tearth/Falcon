using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Falcon.SocketServices.ClientInformations
{
    public class ClientInfo
    {
        public String ClientID { get; set; }
        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public DateTime JoinTime { get; set; }
    }
}
