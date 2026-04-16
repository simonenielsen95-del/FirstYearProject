using NEFAB.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NEFAB.ViewModels;

namespace NEFAB.Commands
{

    public class NavigateToContainerViewCommand : ICommand
    {
        private readonly NavigationStore _navigationStore;

        public NavigateToContainerViewCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        // Implement the CanExecuteChanged event required by ICommand
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new ContainerViewModel();
        }
    }
}
