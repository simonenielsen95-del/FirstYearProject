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

        private Container _selectedNewContainer;
        public Container SelectedNewContainer
        {
            get { return _selectedNewContainer; }
            set { _selectedNewContainer = value; OnPropertyChanged(); }
        }
        private Container _selectedRemoveContainer;
        public Container SelectedRemoveContainer 
        {
            get { return _selectedRemoveContainer; }
            set { _selectedRemoveContainer = value; OnPropertyChanged(); }
        }
        private Container _selectedUpdateContainer;
        public Container SelectedUpdateContainer 
        {
            get { return _selectedUpdateContainer; }
            set { _selectedUpdateContainer = value; OnPropertyChanged(); }
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

            SelectedNewContainer = new Container();
            SelectedRemoveContainer = new Container();
            SelectedUpdateContainer = new Container();
        }

      
        //private ContainerRepository containerRepository = new ContainerRepository();

        //public string ContainerNo { get; set; }
        //public int Week { get; set; }
        //public int Year { get; set; }


        public void CreateNewContainer() 
        {
            try
            {
                _containerService.Add(SelectedNewContainer);
                MessageBox.Show("Ny Container er blevet oprettet!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Container kunne ikke oprettes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }

        public void RemoveContainer() 
        {
            try
            {
                _containerService.Remove(SelectedRemoveContainer);
                MessageBox.Show("Container er blevet slettet!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Container kunne ikke slettes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }
        public void UpdateContainer() 
        {

            try
            {
                _containerService.Update(SelectedUpdateContainer);
                MessageBox.Show("Container er blevet opdateret!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Container kunne ikke opdateres! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }



    }
}

