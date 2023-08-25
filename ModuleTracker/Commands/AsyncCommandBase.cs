using System;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        public override async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception)
            { 
            }
            finally
            {
                IsExecuting = false;
                OnCanExecuteChanged();
            }
        }

        public abstract Task ExecuteAsync(object? parameter);

        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
            }
        }

    }
}
