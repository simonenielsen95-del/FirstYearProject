using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Repositories;
using NEFAB.Services;

using NEFAB.Stores;
using NEFAB.ViewModels;
using NEFAB.Views;


namespace NEFAB.ViewModels
{
    internal class PackageViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }
        public ICommand NavigateToPackageCreateViewCommand { get; }
        public ICommand NavigateToPackageEditViewCommand { get; }


        //private ContainerRepository containerRepository = new ContainerRepository();
        //private PackageRepository packageRepository = new PackageRepository();
        private readonly PackageService _packageService;

        public int Amount { get; set; }
        public int PackageWeight { get; set; }
       public int ProjectNo { get; set; }
        public int ProjectItemNo { get; set; }
        public string SupplierName { get; set; }
        public int InnerQuantity { get; set; }


        private string? _containerNo;
        public string? ContainerNo
        {
            get => _containerNo;
            set
            {
                _containerNo = value;
                OnPropertyChanged();
                FilterPackages();
            }
        }

        private void FilterPackages()
        {
            OCPackages.Clear();

            //if (string.IsNullOrWhiteSpace(ContainerNo))
            //{
            List<Package>? packages = _packageService.GetByContainerNo(ContainerNo);

                foreach (Package package in packages)
                {
                    OCPackages.Add(package);
                }
            //}
        }

        public ObservableCollection<Container> OCContainers { get; set; }
        public ObservableCollection<Package> OCPackages { get; set; } 

        public PackageViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));
            NavigationService packageCreateNavigationService = new NavigationService(navigationStore, () => new PackageCreateViewModel(navigationStore));
            NavigationService packageEditNavigationService = new NavigationService(navigationStore, () => new PackageEditViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);
            NavigateToPackageCreateViewCommand = new NavigateCommand(packageCreateNavigationService);
            NavigateToPackageEditViewCommand = new NavigateCommand(packageEditNavigationService);

            _packageService = new PackageService();

            OCContainers = new ObservableCollection<Container>();
            OCPackages = new ObservableCollection<Package>();

            //LoadAllPackages();
            //LoadAllContainers();
        }
        //public void LoadAllContainers()
        //{
        //    foreach (Container container in containerRepository.GetAll())
        //    {
        //        OCContainers.Add(container);
        //    }
        //}
        //public void LoadAllPackages() 
        //{
        //    foreach (Package package in packageRepository.GetAll())
        //    {
        //        OCPackages.Add(package);
        //    }
        //}
    

    }
}