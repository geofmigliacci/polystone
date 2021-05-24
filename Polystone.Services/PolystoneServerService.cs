using NetCoreServer;
using Polystone.Business;
using Polystone.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Polystone.Services
{
    public sealed class PolystoneServerService : IPolystoneServerService
    {
        private PolystoneServer _polystoneServer;

        public void Start(int port)
        {
            if(_polystoneServer == null)
            {
                _polystoneServer = new PolystoneServer(IPAddress.Any, port);
            }
            _polystoneServer.Start();
        }

        public PolystoneServer GetPolystoneServer()
        {
            if(_polystoneServer == null)
            {
                throw new Exception("PolysoneServer not started!");
            }
            return _polystoneServer;
        }
    }
}
