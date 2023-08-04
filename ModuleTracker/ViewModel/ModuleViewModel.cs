using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleViewModel : BaseViewModel
    {
        public ModuleListingViewModel ModulesListingViewModel { get; }
        public SheetListingViewModel SheetListingViewModel { get; }
        
        public ICommand LoadModuleCommand { get; }

        public ModuleViewModel(ModuleStore moduleStore, SheetStore sheetStore, SelectedModuleStore selectedModuleStore, ModalNavigationStore modalNavigationStore)
        {           
            ModulesListingViewModel = new ModuleListingViewModel(moduleStore, selectedModuleStore, modalNavigationStore);
            SheetListingViewModel = new SheetListingViewModel(selectedModuleStore, sheetStore, modalNavigationStore);           
        }

    }
}
