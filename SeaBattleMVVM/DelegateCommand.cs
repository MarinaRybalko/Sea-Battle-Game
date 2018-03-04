using System;
using System.Windows.Input;

namespace SeaBattleMVVM
{
   public class DelegateCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<object, bool> _canExecuteFunc;
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc == null || CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction?.Invoke(parameter);
        }

        public DelegateCommand(Action<object> executeAction): this(executeAction, null)
        {

        }

        public DelegateCommand(Action<object> executeActionP, Func<object,bool> canExecuteFuncP)
        {
            _executeAction = executeActionP;
            _canExecuteFunc = canExecuteFuncP;
        }
    }
}
