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

        public bool CanSubmit => !string.IsNullOrEmpty(Name);


        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

    }
}
