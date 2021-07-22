using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database.VeiwModel.Commands
{
    class BaseCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        public BaseCommand(Action<object> execute, Func<object, bool> can_execute = null)
        {
            _execute = execute;
            _canExecute = can_execute;
        }
    }
}
