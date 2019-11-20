using System;
using System.Windows.Input;
using Starter.Data.Entities;
using Starter.Framework.Clients;

namespace Starter.Data.Commands
{
    public class CatCommand : ICommand
    {
        //public event EventHandler CanExecuteChanged;

        protected IApiClient ApiClient { get; set; }

        public CatCommand(IApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        //public virtual bool CanExecute(object parameter)
        //{
        //    return false;
        //}

        //public virtual void Execute(object parameter)
        //{
        //}

        private event EventHandler CanExecuteChangedInternal;

        public CatCommand(Action execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public CatCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null && _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void OnCanExecuteChanged()
        {
            var handler = CanExecuteChangedInternal;

            handler?.Invoke(this, EventArgs.Empty);
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }

        private readonly Action _execute;

        private readonly Predicate<object> _canExecute;
    }
}