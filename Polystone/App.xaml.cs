using System.Windows;
using Polystone.Business;
using Polystone.Modules.Home;
using Polystone.Modules.Setting;
using Polystone.Services;
using Polystone.Services.Interfaces;
using Polystone.Views;
using Prism.Ioc;
using Prism.Modularity;
using Syncfusion.Licensing;

namespace Polystone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            SyncfusionLicenseProvider.RegisterLicense("NDQ5NTg3QDMxMzkyZTMxMmUzMGs5OUZ2QjJkb2JydUxQMWkyNTZPeVZ0Mk1GMzZWVUFlNjF2OWt0UHZUN2s9");
            base.InitializeShell(shell);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPolystoneDatabaseService, PolystoneDatabaseService>();
            containerRegistry.RegisterSingleton<IPolystoneServerService, PolystoneServerService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<HomeModule>();
            moduleCatalog.AddModule<SettingModule>();
        }
    }
}
