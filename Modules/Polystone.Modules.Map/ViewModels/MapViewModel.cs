using Microsoft.EntityFrameworkCore;
using Polystone.Business.Interfaces;
using Polystone.Business.Models;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
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

    public class MapViewModel : BindableBase
    {
        private IPolystoneContextService _polystoneContextService;
        private IPolystoneAccountService _polystoneAccountService;

        public ObservableCollection<MapMarker> MapMarkers { get; set; }

        public MapViewModel(
            IPolystoneContextService polystoneContextService,
            IPolystoneAccountService polystoneAccountService
        )
        {
            _polystoneContextService = polystoneContextService;
            _polystoneAccountService = polystoneAccountService;

            Account currentAccount = _polystoneAccountService.GetAccount();
            Account account = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.CurrentHistory).Where(a_ =>
                a_.CurrentHistoryId != null
            ).FirstOrDefault(a_ =>
                a_.Name == currentAccount.Name
            );

            MapMarkers = new ObservableCollection<MapMarker>();
            MapMarkers.Add(new MapMarker()
            {
                LastUpdateDate = account.LastUpdateDate.Value,
                Latitude = account.CurrentHistory.Latitude,
                Longitude = account.CurrentHistory.Longitude,
                Name = account.Name
            });

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Account currentAccount = _polystoneAccountService.GetAccount();
            Account account = _polystoneContextService.GetPolystoneContext().Accounts.AsNoTracking().Include(a_ => a_.CurrentHistory).Where(a_ =>
                a_.CurrentHistoryId != null
            ).FirstOrDefault(a_ =>
                a_.Name == currentAccount.Name
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
    }
}
