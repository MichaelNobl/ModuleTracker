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
        private readonly ModuleStore _moduleStore;
        private readonly SelectedModuleStore _selectedModuleStore;

        public OpenAddSheetCommand(ModalNavigationStore modalNavigationStore, ModuleStore moduleStore, SelectedModuleStore selectedModuleStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _moduleStore = moduleStore;
            _selectedModuleStore = selectedModuleStore;
        }

        public override void Execute(object? parameter)
        {
            var addSheetViewModel = new AddSheetViewModel(_moduleStore, _modalNavigationStore, _selectedModuleStore);
            _modalNavigationStore.CurrentViewModel = addSheetViewModel;
        }
    }
}
