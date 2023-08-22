using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;

namespace ModuleTracker.Wpf.Commands
{
    public class OpenExercisesCommand : CommandBase
    {
        private readonly Sheet _sheet;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ModuleStore _moduleStore;

        public OpenExercisesCommand(Sheet sheet, ModalNavigationStore modalNavigationStore, ModuleStore moduleStore)
        {
            _sheet = sheet;
            _modalNavigationStore = modalNavigationStore;
            _moduleStore = moduleStore;
        }

        public override void Execute(object? parameter)
        {
            var exerciseListingViewModel = new ExercisesViewModel(_sheet, _moduleStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = exerciseListingViewModel;
        }
    }
}
