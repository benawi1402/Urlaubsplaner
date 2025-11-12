using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Urlaubsplaner.Util
{
    /**
     * Taken from the WPF Tutorial Series by Kampa Plays, Episode 24: https://www.youtube.com/watch?v=s7pt3EkDyq4
     * Helps structurize EventHandling in our application by implementing ICommand and adds safeguards, which can be used f.e. to check authorisation 
     */
    class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : ICommand
    {
        private Action<object?> execute = execute;
        private Func<object?, bool>? canExecute = canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
