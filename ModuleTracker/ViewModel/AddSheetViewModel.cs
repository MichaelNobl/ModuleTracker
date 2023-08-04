using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ModuleTracker.Wpf.ViewModel
{
    public class AddSheetViewModel : BaseViewModel
    {
        public bool CanSubmit => !string.IsNullOrEmpty(NumOfExercises) && !string.IsNullOrEmpty(SheetIndex);

        public AddSheetViewModel(SheetStore sheetStore, ModalNavigationStore modalNavigationStore, SelectedModuleStore selectedModuleStore)
        {
            SubmitCommand = new AddSheetCommand(this, sheetStore, modalNavigationStore, selectedModuleStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
        }

        private string _numOfExercises;
        public string NumOfExercises
        {
            get
            {
                return _numOfExercises;
            }
            set
            {
                _numOfExercises = value;
                OnPropertyChanged(nameof(NumOfExercises));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _sheetIndex;
        public string SheetIndex
        {
            get
            {
                return _sheetIndex;
            }
            set
            {
                _sheetIndex = value;
                OnPropertyChanged(nameof(SheetIndex));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
