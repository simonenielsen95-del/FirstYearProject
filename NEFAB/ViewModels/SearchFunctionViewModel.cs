using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Services;
using NEFAB.Stores;

namespace NEFAB.ViewModels
{
    class SearchFunctionViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }

        private readonly SearchFunctionService _searchFunctionService;

        public SearchFunctionViewModel(NavigationStore navigationStore)
        {
            NavigationService homenavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));
            NavigateToHomeViewCommand = new NavigateCommand(homenavigationService);
        }
    }


}
