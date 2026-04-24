using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NEFAB.Commands;
using NEFAB.Stores;
using NEFAB.Services;

namespace NEFAB.ViewModels
{
    internal class PackageCreateViewModel : BaseViewModel
    {
        public ICommand NavigateToPackageViewCommand { get; }

        public PackageCreateViewModel(NavigationStore navigationStore)
        {
            NavigationService packageNavigationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));

            NavigateToPackageViewCommand = new NavigateCommand(packageNavigationService);

        }

    }
}

        
