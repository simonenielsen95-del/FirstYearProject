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
    public class SupplierViewModel : BaseViewModel
    {
        public ICommand SaveSupplierCommand { get; }
        public ICommand NavigateToHomeViewCommand { get; }

        private Supplier _selectedSupplier;
        public Supplier SelectedSupplier
        {
            get { return _selectedSupplier; }
            set { _selectedSupplier = value; OnPropertyChanged(); }
        }

        private readonly SupplierService supplierService;

        public SupplierViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);

            SaveSupplierCommand = new CommandHandler(() => NewSupplier());

            SelectedSupplier = new Supplier();
            supplierService = new SupplierService();
        }
         public void NewSupplier()
        {
            try
            {
                supplierService.Add(SelectedSupplier);
                MessageBox.Show("Ny leverandør er blevet oprettet!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Leverandør kunne ikke oprettes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }

    }
}
