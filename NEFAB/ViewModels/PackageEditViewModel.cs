using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NEFAB.Commands;
using NEFAB.Stores;
using NEFAB.Services;

namespace NEFAB.ViewModels
{
    internal class PackageEditViewModel : BaseViewModel
    {
        public ICommand NavigateToPackageViewCommand { get; }

        public PackageEditViewModel(NavigationStore navigationStore)
        {
            NavigationService packageNaviagationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));

            NavigateToPackageViewCommand = new NavigateCommand(packageNaviagationService);
        }
    }
}