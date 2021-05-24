using Polystone.Business;
using Polystone.Business.Models;
using Polystone.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polystone.Services
{
    public sealed class PolystoneDatabaseService : IPolystoneDatabaseService
    {
        private PolystoneContext _polystoneContext = new PolystoneContext();

        public PolystoneContext GetPolystoneContext()
        {
            return _polystoneContext;
        }
    }
}
