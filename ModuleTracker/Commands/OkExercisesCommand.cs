using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class OkExercisesCommand : AsyncCommandBase
    {
        private ExercisesViewModel _exercisesViewModel;
        private SheetStore _sheetStore;
        private ModalNavigationStore _modalNavigationStore;

        public OkExercisesCommand(ExercisesViewModel exercisesViewModel, SheetStore sheetStore, ModalNavigationStore modalNavigationStore) 
        {
            _exercisesViewModel = exercisesViewModel;
            _sheetStore = sheetStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var sheet = new Sheet(_exercisesViewModel.SheetId, int.Parse(_exercisesViewModel.SheetNumber));

            foreach (var exerciseItem in _exercisesViewModel.ExerciseListingItemViewModel)
            {
                var exercise = new Exercise(exerciseItem.ExerciseId, int.Parse(exerciseItem.ExerciseNumber), exerciseItem.ExerciseIsCompleted);

                sheet.AddExercise(exercise);

                try
                {
                    await _sheetStore.UpdateExercise(exercise);
                }
                catch (Exception)
                {

                }
            }

            try
            {
                await _sheetStore.Update(sheet);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {

            }

            _modalNavigationStore.Close();
        }
    }
}
