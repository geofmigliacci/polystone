using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Polystone.Business
{
    public sealed class PolystoneServer : TcpServer
    {
        public PolystoneServer(IPAddress address, int port) : base(address, port) { }

        protected override NetCoreServer.TcpSession CreateSession() { 
            return new PolystoneSession(this); 
        }
    }
}
