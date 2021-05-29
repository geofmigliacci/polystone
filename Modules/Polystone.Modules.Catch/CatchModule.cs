using Polystone.Modules.Catch.Views;
using Polystone.Services;
using Polystone.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Polystone.Modules.Catch
{
    public class CatchModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Catch>();
        }
    }
}