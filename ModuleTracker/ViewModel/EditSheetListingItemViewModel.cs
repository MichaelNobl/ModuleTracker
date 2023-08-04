using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class EditSheetListingItemViewModel : BaseViewModel
    {
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

		private bool _exerciseDone;
		public bool ExerciseDone	
		{
			get
			{
				return _exerciseDone;
			}
			set
			{
				_exerciseDone = value;
				OnPropertyChanged(nameof(ExerciseDone));
			}
		}

		public EditSheetListingItemViewModel()
		{
            ExerciseDone = false;
            ExerciseNumber = "1";
		}
	}
}
