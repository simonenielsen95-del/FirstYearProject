using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Repositories;
using NEFAB.Repositories.Interfaces;
using NEFAB.Services;
using NEFAB.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NEFAB.ViewModels
{
    public class ContainerViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }

        public ICommand UpdateContainerCommand { get; }
        public ICommand RemoveContainerCommand { get; }
        public ICommand CreateNewContainerCommand { get; }

        private readonly ContainerService _containerService;

        private Container _selectedContainer;
        public Container SelectedContainer
        {
            get { return _selectedContainer; }
            set { _selectedContainer = value; OnPropertyChanged(); }
        }

        public ContainerViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);

            CreateNewContainerCommand = new CommandHandler(() => CreateNewContainer());

            RemoveContainerCommand = new CommandHandler(() => RemoveContainer());

            UpdateContainerCommand = new CommandHandler(() => UpdateContainer());
            //IRepoGetAddUpdateRemove<Container, string> repo = new ContainerRepository(); 
            _containerService = new ContainerService();

            SelectedContainer = new Container();
        }

      
        //private ContainerRepository containerRepository = new ContainerRepository();

        //public string ContainerNo { get; set; }
        //public int Week { get; set; }
        //public int Year { get; set; }


        public void CreateNewContainer() 
        {
            try
            {
                _containerService.Add(SelectedContainer);
                MessageBox.Show("Ny Container er blevet oprettet!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Container kunne ikke oprettes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }

        public void RemoveContainer() { }
        public void UpdateContainer() { }



    }
}

