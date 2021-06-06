using Microsoft.EntityFrameworkCore;
using Polystone.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Polystone.Modules.Account.ViewModels
{
    public class DataTableAccount : BindableBase
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }
        public int Pokecoin { get; set; }
        public int Stardust { get; set; }
        public int PokemonCaught { get; set; }
        public int PokestopSpinned { get; set; }
    }

    public class AccountViewModel : BindableBase, INavigationAware
    {
        private readonly IPolystoneContextService _polystoneContextService;
        private readonly IPolystoneAccountService _polystoneAccountService;

        public ObservableCollection<DataTableAccount> DataTableAccounts { get; set; }
        public DelegateCommand<List<object>> SelectionChangedCommand { get; set; }

        public DispatcherTimer DispatcherTimer { get; set; }

        public AccountViewModel(
            IPolystoneContextService polystoneContextService,
            IPolystoneAccountService polystoneAccountService
        )
        {
            _polystoneContextService = polystoneContextService;
            _polystoneAccountService = polystoneAccountService;

            DataTableAccounts = new ObservableCollection<DataTableAccount>(
                _polystoneContextService.GetPolystoneContext().Accounts.Include(a_ => a_.CurrentHistory).Where(a_ =>
                    a_.Name != null &&
                    a_.CurrentHistory != null
                ).Select(a_ => new DataTableAccount()
                {
                    Id = a_.Id,
                    Name = a_.Name,
                    CreationDate = a_.CreationDate,
                    LastUpdateDate = a_.LastUpdateDate,
                    Level = a_.CurrentHistory.Level,
                    Experience = a_.CurrentHistory.Experience,
                    Pokecoin = a_.CurrentHistory.Pokecoin,
                    Stardust = a_.CurrentHistory.Stardust,
                    PokemonCaught = a_.CurrentHistory.PokemonCaught,
                    PokestopSpinned = a_.CurrentHistory.PokestopSpinned
                })
            );

            if(DataTableAccounts.Count == 1)
            {
                _polystoneAccountService.SetAccount(
                   _polystoneContextService.GetPolystoneContext().Accounts.AsNoTracking().FirstOrDefault(a_ =>
                       a_.Id == DataTableAccounts.FirstOrDefault().Id
                   )
               );
            }

            SelectionChangedCommand = new DelegateCommand<List<object>>(OnSelectionChanged);

            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            DispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            DispatcherTimer.Start();
        }

        private void OnSelectionChanged(List<object> selectedItems)
        {
            if (selectedItems == null || selectedItems[0] == null)
            {
                return;
            }

            DataTableAccount dataTableAccount = (DataTableAccount)((GridRowInfo)selectedItems[0]).RowData;
            if (dataTableAccount == null)
            {
                return;
            }

            _polystoneAccountService.SetAccount(
                _polystoneContextService.GetPolystoneContext().Accounts.AsNoTracking().FirstOrDefault(a_ =>
                    a_.Id == dataTableAccount.Id
                )
            );
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            DataTableAccounts.Clear();
            DataTableAccounts.AddRange(
                _polystoneContextService.GetPolystoneContext().Accounts.AsNoTracking().Include(a_ => a_.CurrentHistory).Where(a_ =>
                    a_.Name != null &&
                    a_.CurrentHistory != null
                ).Select(a_ => new DataTableAccount()
                {
                    Id = a_.Id,
                    Name = a_.Name,
                    CreationDate = a_.CreationDate,
                    LastUpdateDate = a_.LastUpdateDate,
                    Level = a_.CurrentHistory.Level,
                    Experience = a_.CurrentHistory.Experience,
                    Pokecoin = a_.CurrentHistory.Pokecoin,
                    Stardust = a_.CurrentHistory.Stardust,
                    PokemonCaught = a_.CurrentHistory.PokemonCaught,
                    PokestopSpinned = a_.CurrentHistory.PokestopSpinned
                })
            );
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Currently not necessary
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // Currently not necessary
        }
    }
}
