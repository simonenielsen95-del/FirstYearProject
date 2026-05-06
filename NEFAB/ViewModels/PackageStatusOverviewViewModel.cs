using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Services;
using NEFAB.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.IO.Packaging;

using System.Text;
using System.Windows;
using System.Windows.Input;
namespace NEFAB.ViewModels
{
    public class PackageStatusOverviewViewModel : BaseViewModel
    {
        public ICommand NavigateToPackageViewCommand { get; }
        private readonly PackageStatusService _packageStatusService;

        public ObservableCollection<PackageStatus> OCPackageStatus { get; set; }

        private Package _selectedPackage;
        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set { _selectedPackage = value; OnPropertyChanged(); }
        }

        //private PackageStatus _selectedStatus;
        //public PackageStatus SelectedStatus
        //{
        //    get { return _selectedStatus; }
        //    set { _selectedStatus = value; OnPropertyChanged(); }
        //}


        public PackageStatusOverviewViewModel(NavigationStore navigationStore, Package selectedPackage)
        {
            NavigationService packageNavigationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));
            NavigateToPackageViewCommand = new NavigateCommand(packageNavigationService);
            _packageStatusService = new PackageStatusService();

            SelectedPackage = new Package();
            //SelectedStatus = new PackageStatus();
            OCPackageStatus = new ObservableCollection<PackageStatus>();

            SelectedPackage = selectedPackage;
            FilterPackageStatus(selectedPackage);
        }
        public void FilterPackageStatus(Package selectedPackage)
        {
            OCPackageStatus.Clear();
            try
            {
                if (selectedPackage.PackageId != null)
                {
                    foreach (PackageStatus packagestatus in _packageStatusService.GetByPackageId(selectedPackage.PackageId))
                    {
                        OCPackageStatus.Add(packagestatus);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PakkeStatus kunne ikke findes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }
    }
}
