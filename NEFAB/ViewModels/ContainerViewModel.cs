using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.Commands;
using System.Windows.Input;
using NEFAB.Stores;
using NEFAB.Services;

namespace NEFAB.ViewModels
{
    public class ContainerViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }

        public ContainerViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);
        }
    }
}
