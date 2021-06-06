using System.Windows;
using Polystone.Modules.Home;
using Polystone.Modules.Map;
using Polystone.Services;
using Polystone.Services.Interfaces;
using Polystone.Views;
using Prism.Ioc;
using Prism.Modularity;
using Syncfusion.Licensing;
using Prism.DryIoc;
using Prism.Regions;
using System.Windows.Controls;
using Polystone.Core.RegionAdapters;
using Syncfusion.UI.Xaml.NavigationDrawer;
using Polystone.Core;
using Polystone.Modules.Catch;
using Polystone.Modules.Candy;
using Polystone.Modules.Account;
using System;

namespace Polystone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            SyncfusionLicenseProvider.RegisterLicense("NDQ5NTg3QDMxMzkyZTMxMmUzMGs5OUZ2QjJkb2JydUxQMWkyNTZPeVZ0Mk1GMzZWVUFlNjF2OWt0UHZUN2s9");
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPolystoneContextService, PolystoneContextService>();
            containerRegistry.RegisterSingleton<IPolystoneAccountService, PolystoneAccountService>();
            containerRegistry.RegisterSingleton<IPolystoneServerService, PolystoneServerService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AccountModule>();
            moduleCatalog.AddModule<HomeModule>();
            moduleCatalog.AddModule<MapModule>();
            moduleCatalog.AddModule<CatchModule>();
            moduleCatalog.AddModule<CandyModule>();
        }
        
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(SfNavigationDrawer), Container.Resolve<SfNavigationDrawerRegionAdapter>());
        }
    }
}
