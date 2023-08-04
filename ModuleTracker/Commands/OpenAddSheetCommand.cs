using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class OpenAddSheetCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SheetStore _sheetStore;
        private readonly SelectedModuleStore _selectedModuleStore;

        public OpenAddSheetCommand(ModalNavigationStore modalNavigationStore, SheetStore sheetStore, SelectedModuleStore selectedModuleStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _sheetStore = sheetStore;
            _selectedModuleStore = selectedModuleStore;
        }

        public override void Execute(object? parameter)
        {
            var addSheetViewModel = new AddSheetViewModel(_sheetStore, _modalNavigationStore, _selectedModuleStore);
            _modalNavigationStore.CurrentViewModel = addSheetViewModel;
        }
    }
}
