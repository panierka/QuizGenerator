using System;
using System.Windows.Input;

namespace QuizGenerator.ViewModel.Base
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute is { })
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (canExecute is { })
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute is null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }
    }
}
