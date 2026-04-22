using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NEFAB.Commands
{
    public class CommandHandler : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public CommandHandler(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;


        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute();

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
