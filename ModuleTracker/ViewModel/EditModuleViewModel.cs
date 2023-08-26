using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class EditModuleViewModel : BaseViewModel
    {
        public Module Module { get; }

        public EditModuleViewModel(Module module, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {            
            Module = module;
            Name = module.Name;
            SubmitCommand = new EditModuleCommand(this, moduleStore, modalNavigationStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
        }

        #region Properties

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get
            {
                return _isSubmitting;
            }
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        public bool CanSubmit => !string.IsNullOrEmpty(Name);

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        #endregion

        #region Commands
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion
    }
}
