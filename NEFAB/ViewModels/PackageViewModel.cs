using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Services;
using NEFAB.Stores;
using NEFAB.Services;
using NEFAB.Views;
using NEFAB.ViewModels;


namespace NEFAB.ViewModels
{
    internal class PackageViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }
        public ICommand NavigateToPackageCreateViewCommand { get; }

        public ICommand NavigateToPackageEditViewCommand { get; }

        public PackageViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));
            NavigationService packageCreateNavigationService = new NavigationService(navigationStore, () => new PackageCreateViewModel(navigationStore));
            NavigationService packageEditNavigationService = new NavigationService(navigationStore, () => new PackageEditViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);
            NavigateToPackageCreateViewCommand = new NavigateCommand(packageCreateNavigationService);
            NavigateToPackageEditViewCommand = new NavigateCommand(packageEditNavigationService);


        }
    }
}
