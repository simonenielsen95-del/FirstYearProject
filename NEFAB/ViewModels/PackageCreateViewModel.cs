using Microsoft.Win32;
using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Services;
using NEFAB.Stores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NEFAB.ViewModels
{
    internal class PackageCreateViewModel : BaseViewModel
    {
        public ICommand NavigateToPackageViewCommand { get; }

        public ICommand CreateNewPackageCommand { get; }
        public ICommand SelectImageCommand {  get; }

        private readonly PackageService _packageService;


        private BitmapImage _selectedImage;
        public BitmapImage SelectedImage
        {
            get { return _selectedImage; }
            set { _selectedImage = value; OnPropertyChanged(); }
        }

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
            SelectImageCommand = new CommandHandler(() => SelectImage());
            _packageService = new PackageService();

            
            SelectedPackage = new Package();
        }

        public void CreatePackage()
        {
            try
            {
                if (SelectedImage?.UriSource != null)
                {
                    SelectedPackage.Image = SelectedImage.UriSource.LocalPath;
                }
                _packageService.Add(SelectedPackage);
                MessageBox.Show("Ny pakke er blevet oprettet!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pakke kunne ikke oprettes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }
        public void SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Vælg et billede"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Creates a WPF-compatible image from the selected file path
                SelectedImage = new BitmapImage(new Uri(openFileDialog.FileName));
                
            }
        }
    }
}
