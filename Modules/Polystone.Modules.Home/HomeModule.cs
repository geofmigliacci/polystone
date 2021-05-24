using Polystone.Modules.Home.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Polystone.Core;
using Polystone.Services;
using Prism.Mvvm;
using Polystone.Modules.Home.ViewModels;
using Polystone.Services.Interfaces;

namespace Polystone.Modules.Home
{
    public class HomeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public HomeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Views.Home));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<Views.Home, HomeViewModel>();

            containerRegistry.RegisterForNavigation<Views.Home, HomeViewModel>();

            containerRegistry.RegisterSingleton<IPolystoneDatabaseService, PolystoneDatabaseService>();
            containerRegistry.RegisterSingleton<IAccountService, AccountService>();
        }
    }
}