using NEFAB.Commands;
using NEFAB.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NEFAB.Services;

namespace NEFAB.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }

        public EmployeeViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);
        }
    }
}
