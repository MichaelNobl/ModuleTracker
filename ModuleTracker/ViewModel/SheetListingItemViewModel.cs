using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System.Linq;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class SheetListingItemViewModel : BaseViewModel
    {
        public Sheet Sheet { get; private set; }

        public string Name => $"Sheet {Sheet.SheetNumber}";


        public ICommand OpenSheetCommand { get; private set; }

        public SheetListingItemViewModel(Sheet sheet, ModalNavigationStore modalNavigationStore, ModuleStore moduleStore)
        {
            Sheet = sheet;
            NumOfDoneExercises = Sheet.Exercises.Where(x => x.IsCompleted).Count().ToString(); 
            NumOfExercises = Sheet.Exercises.Count.ToString(); 
            OpenSheetCommand = new OpenExercisesCommand(sheet, modalNavigationStore, moduleStore);
        }

        private string _numOfDoneExercises;
        public string NumOfDoneExercises
        {
            get
            {
                return _numOfDoneExercises;
            }
            set
            {
                _numOfDoneExercises = value;
                OnPropertyChanged(nameof(NumOfDoneExercises));
            }
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
            }
        }


    }
}
