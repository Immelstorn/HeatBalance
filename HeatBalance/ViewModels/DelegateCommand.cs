namespace HeatBalance.ViewModels
{
	using System;
	using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private Action _executeMethod;
        private Predicate<object> _canExecuteMethod;

        #region ICommand Members

        public DelegateCommand(Action executeMethod, Predicate<object> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod == null)
                return true;

            return _canExecuteMethod(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _executeMethod();
        }

        #endregion
    }
}
