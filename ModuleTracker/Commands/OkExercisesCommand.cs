using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class OkExercisesCommand : AsyncCommandBase
    {
        private ExercisesViewModel _exercisesViewModel;
        private ModuleStore _moduleStore;
        private ModalNavigationStore _modalNavigationStore;

        public OkExercisesCommand(ExercisesViewModel exercisesViewModel, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore) 
        {
            _exercisesViewModel = exercisesViewModel;
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _exercisesViewModel.ErrorMessage = string.Empty;
            _exercisesViewModel.IsSubmitting = true;

            var sheet = new Sheet(_exercisesViewModel.SheetId, _exercisesViewModel.ModuleId ,int.Parse(_exercisesViewModel.SheetNumber), new List<Exercise>());

            foreach (var exerciseItem in _exercisesViewModel.ExerciseListingItemViewModel)
            {
                var exercise = new Exercise(exerciseItem.ExerciseId, sheet.ModuleId, sheet.Id, int.Parse(exerciseItem.ExerciseNumber), exerciseItem.ExerciseIsCompleted);

                sheet.AddExercise(exercise);

                try
                {
                    await _moduleStore.UpdateExercise(exercise);
                }
                catch
                {
                    _exercisesViewModel.ErrorMessage = "Failed to update exercise. Please try again later.";
                }
            }

            try
            {
                await _moduleStore.UpdateSheet(sheet);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                _exercisesViewModel.ErrorMessage = "Failed to update sheet. Please try again later.";
            }
            finally
            {
                _exercisesViewModel.IsSubmitting = false;
            }
        }
    }
}
