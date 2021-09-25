using System;
using System.Windows.Input;

namespace Demo.Movie.Core.ViewModels.MVVM
{
    public class Command : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public Command(Action<object> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public Command(Action execute) : this(o => execute())
        {
            if (execute == null)
            {
                var nullException = new ArgumentNullException(nameof(execute));

                throw nullException;
            }
        }

        public Command(Action<object> execute, Func<object, bool> canExecute) : this(execute)
        {
            if (canExecute != null)
            {
                _canExecute = canExecute;
            }
            else
            {
                var nullException = new ArgumentNullException(nameof(canExecute));

                throw nullException;
            }
        }

        public Command(Action execute, Func<bool> canExecute) : this(o => execute(), o => canExecute())
        {
            if (execute == null)
            {
                var nullException = new ArgumentNullException(nameof(execute));

                throw nullException;
            }

            if (canExecute == null)
            {
                var nullException = new ArgumentNullException(nameof(canExecute));

                throw nullException;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
