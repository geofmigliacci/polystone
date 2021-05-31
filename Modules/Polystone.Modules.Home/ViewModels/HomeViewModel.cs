using Microsoft.EntityFrameworkCore;
using POGOProtos.Rpc;
using Polystone.Business.Models;
using Polystone.Services;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Polystone.Modules.Home.ViewModels
{
    public class AreaChartModel : BindableBase
    {
        public DateTime Date { get; set; }
        public double Stardust { get; set; }
        public double Experience { get; set; }
        public double PokemonCaught { get; set; }
        public double PokestopSpinned { get; set; }
    }

    public class HomeViewModel : BindableBase
    {
        private IPolystoneContextService _polystoneContextService;
        private IPolystoneAccountService _polystoneAccountService;

        public ObservableCollection<AreaChartModel> AccountHistoryDataPoints { get; set; }
        public ObservableCollection<AreaChartModel> AccountHistoryDayDataPoints { get; set; }
        public ObservableCollection<AreaChartModel> AccountHistoryHourDataPoints { get; set; }

        public HomeViewModel(
            IPolystoneContextService polystoneContextService,
            IPolystoneAccountService polystoneAccountService
        )
        {
            _polystoneContextService = polystoneContextService;
            _polystoneAccountService = polystoneAccountService;

            Account currentAccount = _polystoneAccountService.GetAccount();
            IEnumerable<AccountHistory> accountHistories = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.AccountHistories).FirstOrDefault(
                a_ => a_.Name == currentAccount.Name
            ).AccountHistories.Where(ah_ =>
                ah_.Experience > 0 &&
                ah_.Stardust > 0
            );

            IEnumerable<AccountCatch> accountCatches = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.AccountCatches).FirstOrDefault(
                a_ => a_.Name == currentAccount.Name
            ).AccountCatches.Where(ac_ =>
                ac_.Specie > -1
            );

            AccountHistoryDataPoints = new ObservableCollection<AreaChartModel>();
            AccountHistoryDayDataPoints = new ObservableCollection<AreaChartModel>();
            AccountHistoryHourDataPoints = new ObservableCollection<AreaChartModel>();

            DateTime tomorrow = DateTime.Today.AddDays(1);
            DateTime lastMonth = DateTime.Today.AddMonths(-1);

            while (lastMonth < tomorrow)
            {
                IEnumerable<AccountHistory> currentAccountHistories = accountHistories.Where(ah_ =>
                    ah_.CreationDate.Date == lastMonth.Date
                );
                if (currentAccountHistories.Count() > 0)
                {
                    AccountHistory lastAccountHistory = currentAccountHistories.OrderByDescending(ah_ => ah_.CreationDate).FirstOrDefault();
                    AccountHistoryDataPoints.Add(
                        new AreaChartModel()
                        {
                            Date = lastMonth,
                            Stardust = lastAccountHistory.Stardust,
                            Experience = lastAccountHistory.Experience,
                            PokemonCaught = lastAccountHistory.PokemonCaught,
                            PokestopSpinned = lastAccountHistory.PokestopSpinned
                        }
                    );

                    AccountHistory firstAccountHistory = currentAccountHistories.OrderBy(ah_ => ah_.CreationDate).FirstOrDefault();
                    AccountHistoryDayDataPoints.Add(
                        new AreaChartModel()
                        {
                            Date = lastMonth,
                            Stardust = lastAccountHistory.Stardust - firstAccountHistory.Stardust,
                            Experience = lastAccountHistory.Experience - firstAccountHistory.Experience,
                            PokemonCaught = lastAccountHistory.PokemonCaught - firstAccountHistory.PokemonCaught,
                            PokestopSpinned = lastAccountHistory.PokestopSpinned - firstAccountHistory.PokestopSpinned
                        }
                    );

                    AccountHistoryHourDataPoints.Add(
                        new AreaChartModel()
                        {
                            Date = lastMonth,
                            Stardust = (lastAccountHistory.Stardust - firstAccountHistory.Stardust) / 24,
                            Experience = (lastAccountHistory.Experience - firstAccountHistory.Experience) / 24,
                            PokemonCaught = (lastAccountHistory.PokemonCaught - firstAccountHistory.PokemonCaught) / 24,
                            PokestopSpinned = (lastAccountHistory.PokestopSpinned - firstAccountHistory.PokestopSpinned) / 24
                        }
                    );
                }
                lastMonth = lastMonth.AddDays(1);
            }
        }
    }
}
