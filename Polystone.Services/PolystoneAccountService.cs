using Polystone.Business;
using Polystone.Business.Models;
using Polystone.Services.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Net;

namespace Polystone.Services
{
    [Export(typeof(IPolystoneAccountService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PolystoneAccountService : IPolystoneAccountService
    {
        private Account _account = null;

        public void SetAccount(Account account)
        {
            _account = account;
        }

        public Account GetAccount()
        {
            return _account;
        }

        public bool HasAccount()
        {
            return _account != null;
        }
    }
}
