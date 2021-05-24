using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Polystone.Business;
using Polystone.Core;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Polystone.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ObservableCollection<NavigationItem> Items { get; set; }

        private NavigationItem _selectedItem;
        public NavigationItem SelectedItem
        {
            get { return _selectedItem; }
            set {
                if(_selectedItem != null)
                {
                    ExecuteNavigateCommand(_selectedItem.NavigationPath);
                }
                SetProperty(ref _selectedItem, value); 
            }
        }

        private readonly IRegionManager _regionManager;

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        public MainWindowViewModel(
            IRegionManager regionManager, 
            IPolystoneDatabaseService _polystoneDatabaseService,
            IPolystoneServerService _polystoneServerService)
        {
            _regionManager = regionManager;
            _polystoneDatabaseService.GetPolystoneContext().Database.EnsureCreated();
            _polystoneServerService.Start(9838);

            Items = new ObservableCollection<NavigationItem>
            {
                new NavigationItem()
                {
                    Icon = new Path()
                    {
                        Width = 16,
                        Height = 16,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Data = Geometry.Parse("M6,1.1510006 L6,2.1810011 C3.1380004,2.8610015 0.99999996,5.4320006 1.0000001,8.500001 0.99999996,12.084001 3.9160003,15.000001 7.5,15.000001 11.084,15.000001 14,12.084001 14,8.500001 14,5.4320006 11.861999,2.8610015 9,2.1810011 L9,1.1510006 C12.419006,1.8470016 15,4.8780017 15,8.500001 15,12.634999 11.636002,16.000001 7.5,16.000001 3.3639984,16.000001 -4.4703526E-08,12.634999 0,8.500001 -4.4703526E-08,4.8780017 2.5809936,1.8470016 6,1.1510006 z M7,0 L8,0 8,1.0249994 8,2.0249997 8,5 7,5 7,2.0249997 7,1.0249994 z"),
                        Stretch = Stretch.Fill,
                        Fill = new SolidColorBrush(Colors.Red),
                    },
                    Caption = "Home",
                    NavigationPath = "Home"
                }
            };

            SelectedItem = Items[0];
        }

        void ExecuteNavigateCommand(string navigationpath)
        {
            if(string.IsNullOrEmpty(navigationpath))
            {
                throw new ArgumentNullException();
            }

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationpath);
        }
    }
}
