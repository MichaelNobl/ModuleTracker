using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;

namespace ModuleTracker.Wpf.Commands
{
    public class OpenEditModuleCommand : CommandBase
    {
        private readonly ModuleListingViewModel _moduleListingViewModel;
        private readonly ModuleStore _moduleStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditModuleCommand(ModuleListingViewModel moduleListingViewModel ,ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            _moduleListingViewModel = moduleListingViewModel;
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            var module = _moduleListingViewModel.EditModuleItemViewModel.Module;

            var editModuleViewModel = new EditModuleViewModel(module, _moduleStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editModuleViewModel;
        }
    }
}
