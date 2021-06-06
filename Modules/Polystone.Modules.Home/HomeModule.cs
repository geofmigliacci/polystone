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
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Currently not necessary
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Home>();
        }
    }
}