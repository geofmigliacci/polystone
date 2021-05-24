using Polystone.Business.Models;
using Polystone.Services;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Polystone.Modules.Home.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        public HomeViewModel(IAccountService accountService)
        {
            IList<Account> accounts = accountService.GetAccounts();
        }
    }
}
