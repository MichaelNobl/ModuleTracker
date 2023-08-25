using ModuleTracker.Domain.Models;
using System;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ExerciseListingItemViewModel : BaseViewModel
    {	
		public ExerciseListingItemViewModel(Exercise exercise)
		{
			ExerciseId = exercise.Id;
            ExerciseIsCompleted = exercise.IsCompleted;
            _exerciseNumber = exercise.Number.ToString();
		}

        #region Properties
        public Guid ExerciseId { get; }

        private string _exerciseNumber;
        public string ExerciseNumber
        {
            get
            {
                return _exerciseNumber;
            }
            set
            {
                _exerciseNumber = value;
                OnPropertyChanged(nameof(ExerciseNumber));
            }
        }

        private bool _exerciseIsCompleted;
        public bool ExerciseIsCompleted
        {
            get
            {
                return _exerciseIsCompleted;
            }
            set
            {
                _exerciseIsCompleted = value;
                OnPropertyChanged(nameof(ExerciseIsCompleted));
            }
        }

        #endregion

        #region Methods

        public void Update(Exercise exercise)
		{
			ExerciseIsCompleted = exercise.IsCompleted;
            OnPropertyChanged(nameof(ExerciseIsCompleted));
        }

        #endregion
    }
}
