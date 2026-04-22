using System;
using System.Collections.Generic;
using NEFAB.Domains;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using NEFAB.Commands;
using NEFAB.Repositories;
using NEFAB.Services;
using NEFAB.Stores;
using NEFAB.Repositories.Interfaces;

namespace NEFAB.ViewModels
{
    public class ContainerViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }

        public ICommand UpdateContainerCommand { get; }
        public ICommand RemoveContainerCommand { get; }
        public ICommand CreateNewContainerCommand { get; }

        private readonly ContainerService _containerService;

              public ContainerViewModel(NavigationStore navigationStore)
        {
            NavigationService homeNavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homeNavigationService);

            IRepoGetAddUpdateRemove<Container, string> repo = new ContainerRepository(); _containerService = new ContainerService(repo);
        }

      
        private ContainerRepository containerRepository = new ContainerRepository();

        public string ContainerNo { get; set; }
        public int Week { get; set; }
        public int Year { get; set; }



       

    }
}

