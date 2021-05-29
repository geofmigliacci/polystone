using Polystone.Business;
using Polystone.Core;
using Syncfusion.UI.Xaml.NavigationDrawer;
using Syncfusion.Windows.Shared;

namespace Polystone.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ChromelessWindow
    {
        private readonly IApplicationCommands _applicationCommands;

        public MainWindow(IApplicationCommands applicationCommands)
        {
            InitializeComponent();
            _applicationCommands = applicationCommands;
        }

        private void NavigationDrawer_ItemClicked(object sender, NavigationItemClickedEventArgs e)
        {
            Business.NavigationItem navigationItem = (Business.NavigationItem) e.Item.DataContext;
            if(navigationItem != null)
            {
                _applicationCommands.NavigateCommand.Execute(navigationItem.NavigationPath);
            }
        }
    }
}
