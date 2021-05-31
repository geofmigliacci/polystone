﻿using Microsoft.EntityFrameworkCore;
using POGOProtos.Rpc;
using Polystone.Business.Models;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polystone.Modules.Candy.ViewModels
{
    public class StackingBarChartModel : BindableBase
    {
        public string Specie { get; set; }
        public int Candy { get; set; }
        public int XLCandy { get; set; }
    }

    public class CandyViewModel : BindableBase
    {
        private IPolystoneAccountService _polystoneAccountService;
        private IPolystoneContextService _polystoneContextService;

        public ObservableCollection<StackingBarChartModel> DataTableCandies { get; set; }

        public CandyViewModel(
            IPolystoneContextService polystoneContextService,
            IPolystoneAccountService polystoneAccountService
        )
        {
            _polystoneAccountService = polystoneAccountService;
            _polystoneContextService = polystoneContextService;

            Account currentAccount = _polystoneAccountService.GetAccount();
            Account account = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.AccountCandies).FirstOrDefault(
                a_ => a_.Name == currentAccount.Name
            );

            DataTableCandies = new ObservableCollection<StackingBarChartModel>(account.AccountCandies.Select(c_ => new StackingBarChartModel()
            {
                Specie = ((HoloPokemonId)c_.Specie).ToString("g"),
                Candy = c_.SmallCandy,
                XLCandy = c_.XLCandy
            }));
        }
    }
}
