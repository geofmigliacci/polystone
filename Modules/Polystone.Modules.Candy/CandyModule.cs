using Polystone.Modules.Candy.Views;
using Polystone.Services;
using Polystone.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Polystone.Modules.Candy
{
    public class CandyModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Currently not necessary
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Candy>();
        }
    }
}