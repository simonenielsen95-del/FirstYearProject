using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Repositories;
using NEFAB.Services;
using NEFAB.Stores;
using NEFAB.ViewModels;
using NEFAB.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace NEFAB.ViewModels
{
    internal class PackageViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }
        public ICommand NavigateToPackageCreateViewCommand { get; }
        public ICommand NavigateToPackageEditViewCommand { get; }
        public ICommand NavigateToPackageStatusViewCommand { get; }
        public ICommand RemovePackageCommand { get; }
        public ICommand SearchPackages { get; }

        private Package _selectedPackage;

        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set { _selectedPackage = value; OnPropertyChanged(); }
        }

        private readonly PackageService _packageService;
        private readonly ContainerService _containerService;


        private Container _container;
        public Container Container
        {
            get { return _container; }
            set
            {
                _container = value;
                OnPropertyChanged();

            }
        }

        private int _totalAmount;
        public int TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; OnPropertyChanged(); }
        }

        private int _totalInnerQuantity;
        public int TotalInnerQuantity
        {
            get { return _totalInnerQuantity; }
            set { _totalInnerQuantity = value; OnPropertyChanged(); }
        }

        private void FilterPackages()
        {
            OCPackages.Clear();

            if (Container == null || string.IsNullOrWhiteSpace(Container.ContainerNo))
            {
                return;
            }

            try //uge
            {
                var foundContainer = _containerService.GetByID(Container.ContainerNo);
                if (foundContainer != null)
                {
                    Container = foundContainer;
                }

                int calculatedAmount = 0;
                int calculatedInnerQuantity = 0;

                foreach (Package package in _packageService.GetByContainerNo(Container.ContainerNo)) //beregning
                {
                    OCPackages.Add(package);
                    calculatedAmount += package.Amount ?? 0;
                    calculatedInnerQuantity += package.InnerQuantity ?? 0;
                }

                TotalAmount = calculatedAmount;
                TotalInnerQuantity = calculatedInnerQuantity;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pakker kunne ikke findes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }

        public ObservableCollection<Container> OCContainers { get; set; }
        public ObservableCollection<Package> OCPackages { get; set; }

        public PackageViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));
            NavigationService packageCreateNavigationService = new NavigationService(navigationStore, () => new PackageCreateViewModel(navigationStore));
            NavigationService packageEditNavigationService = new NavigationService(navigationStore, () => new PackageEditViewModel(navigationStore, SelectedPackage));
            NavigationService packageChangeStatusNavigationService = new NavigationService(navigationStore, () => new PackageStatusViewModel(navigationStore, SelectedPackage));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);
            NavigateToPackageCreateViewCommand = new NavigateCommand(packageCreateNavigationService);

            NavigateToPackageEditViewCommand = new CommandHandler(() =>
            {
                if (SelectedPackage != null)
                {
                    var editViewModel = new PackageEditViewModel(navigationStore, SelectedPackage);
                    navigationStore.CurrentViewModel = editViewModel;
                }
            }, () => true);


            NavigateToPackageStatusViewCommand = new CommandHandler(() =>
            {
                if (SelectedPackage != null)
                {
                    var statusViewModel = new PackageStatusViewModel(navigationStore, SelectedPackage);
                    navigationStore.CurrentViewModel = statusViewModel;
                }
            }, () => true);



            SearchPackages = new CommandHandler(() => FilterPackages());
            RemovePackageCommand = new CommandHandler(() => RemovePackage());


            _packageService = new PackageService();
            _containerService = new ContainerService();

            OCContainers = new ObservableCollection<Container>();
            OCPackages = new ObservableCollection<Package>();

            Container = new Container();

        }

        public void RemovePackage()
        {
            if (SelectedPackage != null)
            {
                try
                {
                    _packageService.Remove(SelectedPackage);
                    OCPackages.Remove(SelectedPackage);
                    MessageBox.Show("Pakken er blevet fjernet.", "Success", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Pakken kunne ikke fjernes! {ex}", "Fejl", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Vælg en pakke for at fjerne den.", "Fejl", MessageBoxButton.OK);
            }
        }
    }
}