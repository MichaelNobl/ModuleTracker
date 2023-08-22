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
    public class DeleteSheetCommand : AsyncCommandBase
    {
        private readonly SelectedSheetStore _selectedSheetStore;
        private readonly ModuleStore _moduleStore;

        public DeleteSheetCommand(SelectedSheetStore selectedSheetStore, ModuleStore moduleStore) 
        {
            _selectedSheetStore = selectedSheetStore;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _moduleStore.DeleteSheet(_selectedSheetStore.SelectedSheet.Id);
            }
            catch (Exception)
            {
            }
        }
    }
}
