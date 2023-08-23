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
        public bool CanSubmit => !string.IsNullOrEmpty(NumOfExercises) && !string.IsNullOrEmpty(SheetNumber);

        public AddSheetViewModel(ModuleStore moduleStore, ModalNavigationStore modalNavigationStore, SelectedModuleStore selectedModuleStore)
        {
            SubmitCommand = new AddSheetCommand(this, moduleStore, modalNavigationStore, selectedModuleStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
        }

        #region Properties

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

        private string _sheetNumber;
        public string SheetNumber
        {
            get
            {
                return _sheetNumber;
            }
            set
            {
                _sheetNumber = value;
                OnPropertyChanged(nameof(SheetNumber));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        #endregion

        #region Commands
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion




    }
}
