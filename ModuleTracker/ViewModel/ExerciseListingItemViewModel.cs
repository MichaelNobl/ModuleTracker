using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores.ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ExerciseListingItemViewModel : BaseViewModel
    {
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

		public ExerciseListingItemViewModel(Exercise exercise)
		{

			ExerciseId = exercise.Id;
            ExerciseIsCompleted = exercise.IsCompleted;
            ExerciseNumber = exercise.Number.ToString();
		}

		public void Update(Exercise exercise)
		{
			ExerciseIsCompleted = exercise.IsCompleted;
        }
	}
}
