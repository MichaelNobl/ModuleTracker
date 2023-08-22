using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleListingItemViewModel : BaseViewModel
    {
        public Module Module { get; private set; }

        public string Name => Module.Name;

        public ModuleListingItemViewModel(Module module)
        {
            Module = module;
            NumOfDoneExercises = "1";
            NumOfExercises = "2";
            PercentageDone = "0.5";
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

        private string _percentageDone;

        public string PercentageDone
        {
            get
            {
                return _percentageDone;
            }
            set
            {
                _percentageDone = value;
                OnPropertyChanged(nameof(PercentageDone));
                OnPropertyChanged(nameof(NumOfDoneExercises));
                OnPropertyChanged(nameof(NumOfExercises));
            }
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
