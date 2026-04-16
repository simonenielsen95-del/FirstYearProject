using System;
using System.Collections.Generic;
using System.Text;
using NEFAB.Commands;
using System.Windows.Input;
using NEFAB.Stores;

namespace NEFAB.ViewModels
{

    public class HomeViewModel : BaseViewModel
    {
        public ICommand NavigateToContainerViewCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateToContainerViewCommand = new NavigateToContainerViewCommand(navigationStore);
        }
    }
}
