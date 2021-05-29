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

        public ObservableCollection<MapMarker> MapMarkers { get; set; }

        public MapViewModel(IPolystoneContextService polystoneContextService)
        {
            _polystoneContextService = polystoneContextService;

            Account account = _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.CurrentHistory).Where(a_ =>
                a_.CurrentHistoryId != null
            ).FirstOrDefault(a_ => 
                a_.Name == "Kawaakago"
            );

            MapMarkers = new ObservableCollection<MapMarker>();
            if(account != null)
            {
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
}
