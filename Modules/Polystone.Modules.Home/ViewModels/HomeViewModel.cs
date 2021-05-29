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
    }

    public class DoughnutChartModel : BindableBase
    {
        public string Category { get; set; }

        public int Count { get; set; }
    }

    public class HomeViewModel : BindableBase
    {
        private IPolystoneContextService _polystoneContextService;

        public ObservableCollection<AreaChartModel> AccountHistoryDataPoints { get; set; }
        public ObservableCollection<AreaChartModel> AccountHistoryDayDataPoints { get; set; }
        public ObservableCollection<DoughnutChartModel> AccountCatches { get; set; }

        public HomeViewModel(IPolystoneContextService polystoneContextService)
        {
            _polystoneContextService = polystoneContextService;

            IEnumerable<AccountHistory> accountHistories = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.AccountHistories).FirstOrDefault(
                a_ => a_.Name == "Kawaakago"
            ).AccountHistories.Where(ah_ =>
                ah_.Experience > 0 &&
                ah_.Stardust > 0
            );

            IEnumerable<AccountCatch> accountCatches = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.AccountCatches).FirstOrDefault(
                a_ => a_.Name == "Kawaakago"
            ).AccountCatches.Where(ac_ =>
                ac_.Experience > 0 &&
                ac_.Stardust > 0 &&
                ac_.Specie > -1
            );

            IEnumerable<AccountCandy> accountCandies = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.AccountCandies).FirstOrDefault(
                a_ => a_.Name == "Kawaakago"
            ).AccountCandies;

            int shinies = accountCatches.Count(ac_ => ac_.IsShiny);
            int shadows = accountCatches.Count(ac_ => ac_.IsShadow);
            int both = accountCatches.Count(ac_ =>
                ac_.IsShadow &&
                ac_.IsShiny
            );
            int normals = accountCatches.Count(ac_ =>
                !ac_.IsShadow &&
                !ac_.IsShiny
            );

            AccountCatches = new ObservableCollection<DoughnutChartModel>();
            AccountCatches.Add(new DoughnutChartModel()
            {
                Category = "Shinies",
                Count = shinies
            });
            AccountCatches.Add(new DoughnutChartModel()
            {
                Category = "Shadows",
                Count = shadows
            });
            AccountCatches.Add(new DoughnutChartModel()
            {
                Category = "Shadow Shiny",
                Count = both
            });
            AccountCatches.Add(new DoughnutChartModel()
            {
                Category = "Normal",
                Count = normals
            });

            DateTime tomorrow = DateTime.Today.AddDays(1);
            DateTime lastMonth = DateTime.Today.AddMonths(-1);
            AccountHistoryDataPoints = new ObservableCollection<AreaChartModel>();
            AccountHistoryDayDataPoints = new ObservableCollection<AreaChartModel>();
            while (lastMonth < tomorrow)
            {
                IEnumerable<AccountHistory> currentAccountHistories = accountHistories.Where(ah_ =>
                    ah_.CreationDate.Date == lastMonth.Date
                );
                if(currentAccountHistories.Count() > 0)
                {
                    AccountHistoryDataPoints.Add(
                        new AreaChartModel()
                        {
                            Date = lastMonth,
                            Stardust = Math.Round(currentAccountHistories.Average(ah_ => ah_.Stardust)),
                            Experience = Math.Round(currentAccountHistories.Average(ah_ => ah_.Experience))
                        }
                    );

                    AccountHistory firstAccountHistory = currentAccountHistories.OrderBy(ah_ => ah_.CreationDate).FirstOrDefault();
                    AccountHistory lastAccountHistory = currentAccountHistories.OrderByDescending(ah_ => ah_.CreationDate).FirstOrDefault();
                    AccountHistoryDayDataPoints.Add(
                        new AreaChartModel()
                        {
                            Date = lastMonth,
                            Stardust = lastAccountHistory.Stardust - firstAccountHistory.Stardust,
                            Experience = lastAccountHistory.Experience - firstAccountHistory.Experience,
                        }
                    );
                }
                lastMonth = lastMonth.AddDays(1);
            }
        }
    }
}
