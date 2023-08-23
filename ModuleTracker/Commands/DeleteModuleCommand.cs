using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class DeleteModuleCommand : AsyncCommandBase
    {
        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly ModuleStore _moduleStore;

        public DeleteModuleCommand(SelectedModuleStore selectedModuleStore, ModuleStore moduleStore) 
        {
            _selectedModuleStore = selectedModuleStore;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
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
        }
    }
}
