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
            var sheet = new Sheet(_exercisesViewModel.SheetId, _exercisesViewModel.ModuleId ,int.Parse(_exercisesViewModel.SheetNumber));

            foreach (var exerciseItem in _exercisesViewModel.ExerciseListingItemViewModel)
            {
                var exercise = new Exercise(exerciseItem.ExerciseId, sheet.ModuleId, sheet.Id, int.Parse(exerciseItem.ExerciseNumber), exerciseItem.ExerciseIsCompleted);

                sheet.AddExercise(exercise);

                await _moduleStore.UpdateExercise(exercise);
            }

            try
            {
                await _moduleStore.UpdateSheet(sheet);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {

            }

            _modalNavigationStore.Close();
        }
    }
}
