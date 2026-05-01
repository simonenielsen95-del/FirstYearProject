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
    internal class PackageCreateViewModel : BaseViewModel
    {
        public ICommand NavigateToPackageViewCommand { get; }

        public ICommand CreateNewPackageCommand { get; }

        private readonly PackageService _packageService;

        

        private Package _selectedPackage;
        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set { _selectedPackage = value; OnPropertyChanged(); }
        }

        public PackageCreateViewModel(NavigationStore navigationStore)
        {
            NavigationService packageNavigationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));

            NavigateToPackageViewCommand = new NavigateCommand(packageNavigationService);

            CreateNewPackageCommand = new CommandHandler(() => CreatePackage());
            _packageService = new PackageService();

            
            SelectedPackage = new Package();
        }

        public void CreatePackage()
        {
            try
            {
                _packageService.Add(SelectedPackage);
                MessageBox.Show("Ny pakke er blevet oprettet!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pakke kunne ikke oprettes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }
    }
}
