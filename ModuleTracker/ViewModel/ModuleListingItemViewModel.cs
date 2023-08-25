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

        public string NumOfDoneExercises => CalculateDoneExercises();

        public string NumOfExercises => CalculateExercises();

        #endregion

        #region Methods
        public override void Dispose()
        {
            _moduleStore.SheetUpdated -= SelectedModuleStoreSheetUpdated;
            _selectedSheetStore.SelectedSheetChanged -= SelectedModuleStoreSelectedSheetChanged;

            base.Dispose();
        }

        private string CalculateDoneExercises()
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

            return counter.ToString();
        }

        private string CalculateExercises()
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

            return counter.ToString();
        }

        private void SelectedModuleStoreSelectedSheetChanged()
        {
            OnPropertyChanged(nameof(NumOfExercises));
            OnPropertyChanged(nameof(NumOfDoneExercises));
        }

        private void SelectedModuleStoreSheetUpdated(Sheet obj)
        {
            OnPropertyChanged(nameof(NumOfExercises));
            OnPropertyChanged(nameof(NumOfDoneExercises));
        }

        #endregion


    }
}
