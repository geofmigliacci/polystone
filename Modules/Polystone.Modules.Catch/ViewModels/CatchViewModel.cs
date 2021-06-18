using Microsoft.EntityFrameworkCore;
using POGOProtos.Rpc;
using Polystone.Business.Models;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polystone.Modules.Catch.ViewModels
{
    public class DataTableCatch : BindableBase
    {
        public string Specie { get; set; }
        public DateTime CreationTimeMs { get; set; }
        public int Cp { get; set; }
        public int Experience { get; set; }
        public int Stardust { get; set; }
        public bool IsShiny { get; set; }
        public bool IsShadow { get; set; }
    }

    public class CatchViewModel : BindableBase, INavigationAware
    {
        private readonly IPolystoneContextService _polystoneContextService;
        private readonly IPolystoneAccountService _polystoneAccountService;

        public Account CurrentAccount { get; set; }

        public ObservableCollection<DataTableCatch> DataTableCatches { get; set; }

        public CatchViewModel(
            IPolystoneContextService polystoneContextService,
            IPolystoneAccountService polystoneAccountService
        )
        {
            _polystoneAccountService = polystoneAccountService;
            _polystoneContextService = polystoneContextService;

            CurrentAccount = _polystoneAccountService.GetAccount();
            IEnumerable<AccountCatch> accountCatches = _polystoneContextService.GetPolystoneContext().Accounts.AsNoTracking().Include(a_ => a_.AccountCatches).FirstOrDefault(
                a_ => a_.Name == CurrentAccount.Name
            ).AccountCatches.Where(c_ => 
                c_.PokemonId > -1
            );

            DataTableCatches = new ObservableCollection<DataTableCatch>(accountCatches.Select(c_ => new DataTableCatch()
            {
                Specie = ((HoloPokemonId)c_.PokemonId).ToString("g"),
                CreationTimeMs = c_.CreationTimeMs.Value,
                Cp = c_.Cp,
                Experience = c_.Experience,
                Stardust = c_.Stardust,
                IsShiny = c_.IsShiny,
                IsShadow = c_.IsShadow,
            }));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Currently not necessary
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return _polystoneAccountService.HasAccount();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // Currently not necessary
        }
    }
}
