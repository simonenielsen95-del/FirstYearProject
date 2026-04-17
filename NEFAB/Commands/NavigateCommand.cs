using NEFAB.Stores;
using NEFAB.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NEFAB.Services;

namespace NEFAB.Commands
{
    public class NavigateCommand : ICommand
    {
        private readonly NavigationService _navigationService;


        public NavigateCommand(NavigationService navigationService)
        {
            //Opdater din constructor så den initialiserer det nye NavigationService field.
            _navigationService = navigationService;
        
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            
            _navigationService.Navigate();
        }
    }
}
