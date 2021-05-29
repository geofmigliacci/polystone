using Microsoft.EntityFrameworkCore;
using POGOProtos.Rpc;
using Polystone.Business.Models;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polystone.Modules.Catch.ViewModels
{
    public class StackingBarChartModel : BindableBase
    {
        public string Specie { get; set; }
        public int Candy { get; set; }
        public int XLCandy { get; set; }
    }
    public class DataTableCatch : BindableBase
    {
        public string Specie { get; set; }
        public DateTime CatchDate { get; set; }
        public int Cp { get; set; }
        public int Experience { get; set; }
        public int Stardust { get; set; }
        public bool IsShiny { get; set; }
        public bool IsShadow { get; set; }
    }

    public class CatchViewModel : BindableBase
    {
        private IPolystoneAccountService _polystoneAccountService;
        private IPolystoneContextService _polystoneContextService;

        public ObservableCollection<DataTableCatch> DataTableCatches { get; set; }

        public CatchViewModel(
            IPolystoneContextService polystoneContextService,
            IPolystoneAccountService polystoneAccountService
        )
        {
            _polystoneAccountService = polystoneAccountService;
            _polystoneContextService = polystoneContextService;

            Account currentAccount = _polystoneAccountService.GetAccount();
            IEnumerable<AccountCatch> accountCatches = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.AccountCatches).FirstOrDefault(
                a_ => a_.Name == currentAccount.Name
            ).AccountCatches.Where(c_ => 
                c_.Specie > -1
            );

            DataTableCatches = new ObservableCollection<DataTableCatch>(accountCatches.Select(c_ => new DataTableCatch()
            {
                Specie = ((HoloPokemonId)c_.Specie).ToString("g"),
                CatchDate = c_.CatchDate.Value,
                Cp = c_.Cp,
                Experience = c_.Experience,
                Stardust = c_.Stardust,
                IsShiny = c_.IsShiny,
                IsShadow = c_.IsShadow,
            }));
        }
    }
}
