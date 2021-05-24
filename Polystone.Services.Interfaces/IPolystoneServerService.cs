using Polystone.Business.Models;
using Polystone.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polystone.Services.Interfaces
{
    public interface IPolystoneServerService
    {
        public void Start(int port);

        public PolystoneServer GetPolystoneServer();
    }
}
