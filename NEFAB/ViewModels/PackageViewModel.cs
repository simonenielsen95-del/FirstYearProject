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

        public ICommand SearchPackages {  get; }

        private Package _selectedPackage;

        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set { _selectedPackage = value; OnPropertyChanged(); }
        }

        private readonly PackageService _packageService;



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

        private void FilterPackages()
        {
            OCPackages.Clear();

            if (Container == null || string.IsNullOrWhiteSpace(Container.ContainerNo))
            {
                return;
            }

            try
            {
                foreach (Package package in _packageService.GetByContainerNo(Container.ContainerNo))
                {
                    OCPackages.Add(package);
                }
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

            SearchPackages = new CommandHandler(() => FilterPackages());

            _packageService = new PackageService();

            OCContainers = new ObservableCollection<Container>();
            OCPackages = new ObservableCollection<Package>();

            Container = new Container();
            
        }
       

    }
}