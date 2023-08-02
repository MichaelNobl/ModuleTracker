using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleListingViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ModuleListingItemViewModel> _moduleListingItemViewModel;

        public IEnumerable<ModuleListingItemViewModel> ModuleListingItemViewModel =>
            _moduleListingItemViewModel;       

        public ModuleListingViewModel()
        {
            _moduleListingItemViewModel = new ObservableCollection<ModuleListingItemViewModel>();

            _moduleListingItemViewModel.Add(new ModuleListingItemViewModel(new Module(new Guid(), "Höhere Analysis", new List<Sheet>())));
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
