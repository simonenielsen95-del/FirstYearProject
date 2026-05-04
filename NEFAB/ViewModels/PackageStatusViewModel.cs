using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Services;
using NEFAB.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace NEFAB.ViewModels
{
    public class PackageStatusViewModel : BaseViewModel
    {
        public ICommand NavigateToPackageViewCommand { get; }
        public ICommand ChangeStatusPackageCommand { get; }

        private readonly PackageService _packageService;

        private Package _selectedPackage;
        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set { _selectedPackage = value; OnPropertyChanged(); }
        }


        public PackageStatusViewModel(NavigationStore navigationStore, Package selectedPackage)
        {
            NavigationService packageNavigationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));

            NavigateToPackageViewCommand = new NavigateCommand(packageNavigationService);

            ChangeStatusPackageCommand = new CommandHandler(() => ChangeStatusPackage());
            _packageService = new PackageService();

            SelectedPackage = new Package();

            SelectedPackage = selectedPackage;
        }

        public void ChangeStatusPackage()
        {
            try
            {
                _packageService.Update(SelectedPackage);
                MessageBox.Show("Status på pakken er blevet opdateret", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Status på pakken kan ikke opdateres! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }
    }
}

