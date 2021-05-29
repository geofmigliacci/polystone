using Polystone.Core;
using Polystone.Modules.Map.ViewModels;
using Polystone.Modules.Map.Views;
using Polystone.Services;
using Polystone.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace Polystone.Modules.Map
{
    public class MapModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MapModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Map>();
        }
    }
}