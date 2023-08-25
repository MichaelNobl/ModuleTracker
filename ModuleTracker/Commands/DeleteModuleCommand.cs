using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class DeleteModuleCommand : AsyncCommandBase
    {
        private readonly ModuleListingViewModel _moduleListingViewModel;
        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly ModuleStore _moduleStore;

        public DeleteModuleCommand(ModuleListingViewModel moduleListingViewModel, SelectedModuleStore selectedModuleStore, ModuleStore moduleStore) 
        {
            _moduleListingViewModel = moduleListingViewModel;
            _selectedModuleStore = selectedModuleStore;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _moduleListingViewModel.IsDeleting = true;

            try
            {
                if(_selectedModuleStore.SelectedModule != null)
                {
                    await _moduleStore.DeleteModule(_selectedModuleStore.SelectedModule.Id);
                }                
            }
            catch (Exception)
            {
            }
            finally
            {
                _moduleListingViewModel.IsDeleting = false;
            }
        }
    }
}
