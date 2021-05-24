using Polystone.Business;
using Polystone.Business.Models;
using Polystone.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polystone.Services
{
    public sealed class AccountService : IAccountService
    {
        private IPolystoneDatabaseService _polystoneDatabaseService;

        public AccountService(IPolystoneDatabaseService polystoneDatabaseService)
        {
            _polystoneDatabaseService = polystoneDatabaseService;
        }

        public IList<Account> GetAccounts()
        {
            return _polystoneDatabaseService.GetPolystoneContext().Accounts.ToList();
        }
    }
}
