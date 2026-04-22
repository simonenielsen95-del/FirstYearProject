using NEFAB.Commands;
using NEFAB.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NEFAB.Services;
using NEFAB.Domains;

namespace NEFAB.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        public ICommand SaveSupplierCommand { get; }
        public ICommand NavigateToHomeViewCommand { get; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(); }
        }

        public SupplierViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);

            SaveSupplierCommand = new CommandHandler()
        }

    }
}
