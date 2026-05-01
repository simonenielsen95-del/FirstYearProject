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
    internal class PackageEditViewModel : BaseViewModel
    {
        public ICommand NavigateToPackageViewCommand { get; }
        public ICommand EditPackageCommand { get; }

        private readonly PackageService _packageService;

        

        private Package _selectedPackage;
        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set { _selectedPackage = value; OnPropertyChanged(); }
        }

        public PackageEditViewModel(NavigationStore navigationStore, Package selectedPackage)
        {
            NavigationService packageNavigationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));

            NavigateToPackageViewCommand = new NavigateCommand(packageNavigationService);

            EditPackageCommand = new CommandHandler(() => EditPackage());
            _packageService = new PackageService();

            SelectedPackage = new Package();

            SelectedPackage = selectedPackage;
        }

        public void EditPackage()
        {
            try
            {
                _packageService.Update(SelectedPackage);
                MessageBox.Show("Pakken er blevet opdateret", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pakken kan ikke opdateres! {ex}", "Fejl", MessageBoxButton.OK);
            }

        }
    }
}