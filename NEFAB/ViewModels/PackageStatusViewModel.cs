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
        public ICommand AddStatusPackageCommand { get; }

        private readonly PackageStatusService _packageStatusService;

        private Package _selectedPackage;
        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set { _selectedPackage = value; OnPropertyChanged(); }
        }

        public Array AvailableStatusTypes => Enum.GetValues(typeof(StatusType));

        private PackageStatus _selectedStatus;
        public PackageStatus SelectedStatus
        {
            get { return _selectedStatus; }
            set { _selectedStatus = value; OnPropertyChanged(); }
        }


        public PackageStatusViewModel(NavigationStore navigationStore, Package selectedPackage)
        {
            NavigationService packageNavigationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));

            NavigateToPackageViewCommand = new NavigateCommand(packageNavigationService);

            AddStatusPackageCommand = new CommandHandler(() => AddStatusPackage());
            _packageStatusService = new PackageStatusService();

            SelectedPackage = new Package();

            SelectedPackage = selectedPackage;



            //if (Enum.TryParse(SelectedStatus.Status, out parsedStatus))
            //{
            //    SelectedStatus = parsedStatus;
            //}
        }

        public void AddStatusPackage()
        {
            try
            {
                //SelectedStatus.Status = SelectedStatus.ToString();

                //var statusObj = new PackageStatus 
                //{ 
                //    Status = SelectedStatus,
                //    PackageId = SelectedPackage.PackageId,
                //    Comment = SelectedPackage.Comment
                //};
                SelectedStatus.PackageId = SelectedPackage.PackageId;
                _packageStatusService.Add(SelectedStatus);

                MessageBox.Show("Status på pakken er blevet oprettet", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Status på pakken kan ikke opdateres! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }
    }
}

