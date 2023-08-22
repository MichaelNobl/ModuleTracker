using ModuleTracker.Wpf.Stores;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleViewModel : BaseViewModel
    {
        public ModuleListingViewModel ModulesListingViewModel { get; }
        public SheetListingViewModel SheetListingViewModel { get; }

        public ICommand LoadModuleCommand { get; }

        public ModuleViewModel(ModuleStore moduleStore, SelectedModuleStore selectedModuleStore, SelectedSheetStore selectedSheetStore,ModalNavigationStore modalNavigationStore)
        {
            ModulesListingViewModel = new ModuleListingViewModel(moduleStore, selectedModuleStore, modalNavigationStore);
            SheetListingViewModel = new SheetListingViewModel(moduleStore, selectedModuleStore, selectedSheetStore, modalNavigationStore);
        }
    }
}
