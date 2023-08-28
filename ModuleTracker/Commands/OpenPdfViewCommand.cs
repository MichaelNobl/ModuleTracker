using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;

namespace ModuleTracker.Wpf.Commands
{
    class OpenPdfViewCommand : CommandBase
    {
        private readonly SheetListingItemViewModel _sheetListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ModuleStore _moduleStore;

        public OpenPdfViewCommand(SheetListingItemViewModel sheetListingItemViewModel, ModalNavigationStore modalNavigationStore, ModuleStore moduleStore)
        {
            _sheetListingItemViewModel = sheetListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _moduleStore = moduleStore;
        }

        public override void Execute(object? parameter)
        {
            var openPdfViewModel = new OpenPdfViewModel(_sheetListingItemViewModel, _modalNavigationStore, _moduleStore);
            _modalNavigationStore.CurrentViewModel = openPdfViewModel;                        
        }
    }
}
