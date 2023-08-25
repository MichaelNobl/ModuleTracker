using ModuleTracker.Wpf.ViewModel;
using System;

namespace ModuleTracker.Wpf.Stores
{
    public class ModalNavigationStore
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if( _currentViewModel != null )
                {
                    _currentViewModel.Dispose();
                }                
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public bool IsOpen => _currentViewModel != null;

        public event Action CurrentViewModelChanged;

        internal void Close()
        {
            CurrentViewModel = null;
        }
    }
}
