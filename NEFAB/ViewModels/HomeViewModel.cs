using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.Commands;
using System.Windows.Input;
using NEFAB.Stores;
using NEFAB.Services;

namespace NEFAB.ViewModels
{

    public class HomeViewModel : BaseViewModel
    {
        public ICommand NavigateToContainerViewCommand { get; }
        public ICommand NavigateToEmployeeViewCommand { get; }

        public ICommand NavigateToSupplierViewCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigationService containerNavigationService = new NavigationService(navigationStore, ()=> new ContainerViewModel(navigationStore));
            NavigationService employeeNavigationService = new NavigationService(navigationStore, () => new EmployeeViewModel(navigationStore));
            NavigationService supplierNavigationService = new NavigationService(navigationStore, () => new SupplierViewModel(navigationStore));

            NavigateToContainerViewCommand = new NavigateCommand(containerNavigationService);
            NavigateToEmployeeViewCommand = new NavigateCommand(employeeNavigationService);
            NavigateToSupplierViewCommand = new NavigateCommand(supplierNavigationService);
        }
    }
}
