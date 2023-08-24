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
        private readonly SheetListingViewModel _sheetListingViewModel;
        private readonly SelectedSheetStore _selectedSheetStore;
        private readonly ModuleStore _moduleStore;

        public DeleteSheetCommand(SheetListingViewModel sheetListingViewModel, SelectedSheetStore selectedSheetStore, ModuleStore moduleStore) 
        {
            _sheetListingViewModel = sheetListingViewModel;
            _selectedSheetStore = selectedSheetStore;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _sheetListingViewModel.IsDeleting = true;

            try
            {
                if (_selectedSheetStore.SelectedSheet != null)
                {
                    await _moduleStore.DeleteSheet(_selectedSheetStore.SelectedSheet.Id);
                }                
            }
            catch (Exception)
            {
            }
            finally
            {
                _sheetListingViewModel.IsDeleting = false;
            }
        }
    }
}
