using System;
using System.Windows.Input;

namespace CryptocurrencyInformationApp.ViewModels
{
    public class ViewModelCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Predicate<object>? _canExecuteAction;

        public ViewModelCommand(Action<object> executeAction, Predicate<object>? canExecuteAction = null)
        {
            _canExecuteAction = canExecuteAction;
            _executeAction = executeAction;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecuteAction is null ? true : _canExecuteAction(parameter!);
        }

        public void Execute(object? parameter)
        {
            _executeAction?.Invoke(parameter!);
        }
    }
}
