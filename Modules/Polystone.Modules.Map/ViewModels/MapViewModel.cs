using Microsoft.EntityFrameworkCore;
using Polystone.Business.Models;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Polystone.Modules.Map.ViewModels
{
    public class MapMarker : BindableBase
    {
        public DateTime LastUpdateDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
    }

    public class MapViewModel : BindableBase, INavigationAware
    {
        private IPolystoneContextService _polystoneContextService;
        private IPolystoneAccountService _polystoneAccountService;

        public Account CurrentAccount { get; set; }

        public ObservableCollection<MapMarker> MapMarkers { get; set; }

        public DispatcherTimer DispatcherTimer { get; set; }

        public MapViewModel(
            IPolystoneContextService polystoneContextService,
            IPolystoneAccountService polystoneAccountService
        )
        {
            _polystoneContextService = polystoneContextService;
            _polystoneAccountService = polystoneAccountService;

            CurrentAccount = _polystoneAccountService.GetAccount();
            Account account = _polystoneContextService.GetPolystoneContext().Accounts.AsNoTracking().Include(a_ => a_.CurrentHistory).Where(a_ =>
                a_.CurrentHistoryId != null
            ).FirstOrDefault(a_ =>
                a_.Name == CurrentAccount.Name
            );

            MapMarkers = new ObservableCollection<MapMarker>();
            MapMarkers.Add(new MapMarker()
            {
                LastUpdateDate = account.LastUpdateDate.Value,
                Latitude = account.CurrentHistory.Latitude,
                Longitude = account.CurrentHistory.Longitude,
                Name = account.Name
            });

            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            DispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            DispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Account account = _polystoneContextService.GetPolystoneContext().Accounts.AsNoTracking().Include(a_ => a_.CurrentHistory).Where(a_ =>
                a_.CurrentHistoryId != null
            ).FirstOrDefault(a_ =>
                a_.Name == CurrentAccount.Name
            );

            MapMarkers.Clear();
            MapMarkers.Add(new MapMarker()
            {
                LastUpdateDate = account.LastUpdateDate.Value,
                Latitude = account.CurrentHistory.Latitude,
                Longitude = account.CurrentHistory.Longitude,
                Name = account.Name
            });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return _polystoneAccountService.HasAccount();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
