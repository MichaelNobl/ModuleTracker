using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using System;
using System.Linq;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleListingItemViewModel : BaseViewModel
    {
        private ModuleStore _moduleStore;
        private SelectedSheetStore _selectedSheetStore;

        public ModuleListingItemViewModel(Module module, ModuleStore moduleStore, SelectedSheetStore selectedSheetStore)
        {
            Module = module;
            _moduleStore = moduleStore;
            _selectedSheetStore = selectedSheetStore;

            _moduleStore.SheetUpdated += SelectedModuleStoreSheetUpdated;

            _selectedSheetStore.SelectedSheetChanged += SelectedModuleStoreSelectedSheetChanged;
        }        

        #region Properties
        public Module Module { get; private set; }

        public string Name => Module.Name;

        public string ExercisePercentage => CalculateExercisePercentage();        

        #endregion

        #region Methods
        public override void Dispose()
        {
            _moduleStore.SheetUpdated -= SelectedModuleStoreSheetUpdated;
            _selectedSheetStore.SelectedSheetChanged -= SelectedModuleStoreSelectedSheetChanged;

            base.Dispose();
        }
        private string CalculateExercisePercentage()
        {
            var doneExercises = CalculateDoneExercises();

            var exercises = CalculateExercises();

            if(Math.Abs(exercises) < 1e-12)
            {
                return "0";
            }

            var percentage = doneExercises / exercises * 100;

            return String.Format("{0:0}", percentage);
        }


        private double CalculateDoneExercises()
        {
            var counter = 0;

            var module = _moduleStore.Modules.FirstOrDefault(m => m.Id == Module.Id);

            if(module != null)
            {
                foreach (var sheet in module.Sheets)
                {
                    foreach (var exercise in sheet.Exercises)
                    {
                        if (exercise.IsCompleted)
                        {
                            counter++;
                        }
                    }
                }
            }                    

            return (double)counter;
        }

        private double CalculateExercises()
        {
            var counter = 0;

            var module = _moduleStore.Modules.FirstOrDefault(m => m.Id == Module.Id);

            if( module != null)
            {
                foreach (var sheet in module.Sheets)
                {
                    foreach (var exercise in sheet.Exercises)
                    {
                        counter++;
                    }
                }
            }

            return (double)counter;
        }

        private void SelectedModuleStoreSelectedSheetChanged()
        {
            OnPropertyChanged(nameof(ExercisePercentage));
        }

        private void SelectedModuleStoreSheetUpdated(Sheet obj)
        {
            OnPropertyChanged(nameof(ExercisePercentage));
        }

        #endregion

    }
}
