using ModuleTracker.Wpf.Stores;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleViewModel : BaseViewModel
    {       
        public ModuleViewModel(ModuleStore moduleStore, SelectedModuleStore selectedModuleStore, SelectedSheetStore selectedSheetStore, ModalNavigationStore modalNavigationStore)
        {
            ModulesListingViewModel = new ModuleListingViewModel(moduleStore, selectedModuleStore, selectedSheetStore, modalNavigationStore);
            SheetListingViewModel = new SheetListingViewModel(moduleStore, selectedModuleStore, selectedSheetStore, modalNavigationStore);
        }
        public ModuleListingViewModel ModulesListingViewModel { get; }
        public SheetListingViewModel SheetListingViewModel { get; }

        public ICommand LoadModuleCommand { get; }
    }
}
