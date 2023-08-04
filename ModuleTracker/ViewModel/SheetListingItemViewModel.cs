using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class SheetListingItemViewModel : BaseViewModel
    {
        public Sheet Sheet { get; private set; }

        public string Name => $"Sheet {Sheet.SheetNumber}";

        public ICommand OpenSheetCommand { get; private set; }

        public SheetListingItemViewModel(Sheet sheet)
        {
            Sheet = sheet;
            _numOfDoneExercises = "0";
            _numOfExercises = "1";
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
