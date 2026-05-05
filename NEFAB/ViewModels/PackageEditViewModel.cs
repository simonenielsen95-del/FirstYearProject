using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Services;
using NEFAB.Stores;

namespace NEFAB.ViewModels
{
    internal class PackageEditViewModel : BaseViewModel
    {
        public ICommand NavigateToPackageViewCommand { get; }
        public ICommand EditPackageCommand { get; }
        public ICommand SelectImageCommand { get; }

        private readonly PackageService _packageService;

        private BitmapImage? _selectedImage;
        public BitmapImage? SelectedImage
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

        public PackageEditViewModel(NavigationStore navigationStore, Package selectedPackage)
        {
            NavigationService packageNavigationService = new NavigationService(navigationStore, () => new PackageViewModel(navigationStore));

            NavigateToPackageViewCommand = new NavigateCommand(packageNavigationService);

            EditPackageCommand = new CommandHandler(() => EditPackage());
            SelectImageCommand = new CommandHandler(() => SelectImage());

            _packageService = new PackageService();

            SelectedPackage = new Package();

            SelectedPackage = selectedPackage;

            if (SelectedPackage.Image != null)
            {
                SelectedImage = SelectedPackage.ToImage(SelectedPackage.Image);
            }
        }

        public void EditPackage()
        {
            if (SelectedImage?.UriSource != null)
            {
                SelectedPackage.Image = System.IO.File.ReadAllBytes(SelectedImage.UriSource.LocalPath);
            }
            try
            {
                _packageService.Update(SelectedPackage);
                MessageBox.Show("Pakken er blevet opdateret", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pakken kan ikke opdateres! {ex}", "Fejl", MessageBoxButton.OK);
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