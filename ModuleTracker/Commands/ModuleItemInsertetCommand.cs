using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class ModuleItemInsertetCommand : AsyncCommandBase
    {
        private readonly ModuleListingViewModel _moduleListingViewModel;
        private readonly ModuleStore _moduleStore;

        public ModuleItemInsertetCommand(ModuleListingViewModel moduleListingViewModel, ModuleStore moduleStore)
        {
            _moduleListingViewModel = moduleListingViewModel;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _moduleListingViewModel.ErrorMessage = string.Empty;

            var insertetModule = _moduleStore.Modules.SingleOrDefault(m => m.Id == _moduleListingViewModel.InsertetModuleItemViewModel.Module.Id);
            var targetetModule = _moduleStore.Modules.SingleOrDefault(m => m.Id == _moduleListingViewModel.TargetetModuleItemViewModel.Module.Id);
            
            if(insertetModule != null && targetetModule != null)
            {
                try
                {
                    await _moduleStore.ReorderModules(insertetModule, targetetModule);
                }
                catch (Exception)
                {
                    _moduleListingViewModel.ErrorMessage = "Failed to reorder modules. Please try again later.";
                    return;
                }
            }         
        }
    }
}
