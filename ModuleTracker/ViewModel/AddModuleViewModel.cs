using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class AddModuleViewModel : BaseViewModel
    {
        public AddModuleViewModel(ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            SubmitCommand = new AddModuleCommand(this, moduleStore, modalNavigationStore);
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

        #endregion

        #region Commands
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion
    }
}
