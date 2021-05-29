using Polystone.Business;
using Polystone.Services.Interfaces;
using System;
using System.Net;

namespace Polystone.Services
{
    public class PolystoneServerService : IPolystoneServerService
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
