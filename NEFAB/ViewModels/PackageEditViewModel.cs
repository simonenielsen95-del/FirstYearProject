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

        private Container _selectedContainer;
        public Container SelectedContainer
        {
            get { return _selectedContainer; }
            set { _selectedContainer = value; OnPropertyChanged(); }
        }

        private Supplier _selectedSupplier;
        public Supplier SelectedSupplier
        {
            get { return _selectedSupplier; }
            set { _selectedSupplier = value; OnPropertyChanged(); }
        }

        private Package _selectedPackage;
        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set { _selectedPackage = value; OnPropertyChanged(); }
        }

        public PackageEditViewModel(NavigationStore navigationStore)
        {
            NavigationService packageNavigationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));

            NavigateToPackageViewCommand = new NavigateCommand(packageNavigationService);

            EditPackageCommand = new CommandHandler(() => EditPackage());
            _packageService = new PackageService();

            SelectedContainer = new Container();
            SelectedSupplier = new Supplier();
            SelectedPackage = new Package();
        }

        public void EditPackage()
        {
            try
            {
                _packageService.Add(SelectedContainer, SelectedSupplier, SelectedPackage);
                MessageBox.Show("Pakken er blevet opdateret", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pakken kan ikke opdateres! {ex}", "Fejl", MessageBoxButton.OK);
            }

        }
    }
}