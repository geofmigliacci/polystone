using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Polystone.Modules.Setting
{
    public class SettingModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public SettingModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}