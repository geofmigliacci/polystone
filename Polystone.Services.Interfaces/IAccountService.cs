using Polystone.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polystone.Services.Interfaces
{
    public interface IAccountService 
    {
        public IList<Account> GetAccounts();
    }
}
