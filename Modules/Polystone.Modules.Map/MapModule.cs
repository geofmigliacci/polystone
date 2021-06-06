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
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Currently not necessary
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Map>();
        }
    }
}