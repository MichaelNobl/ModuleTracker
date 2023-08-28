using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ModuleTracker.Wpf.Commands
{
    public class DeleteSheetCommand : AsyncCommandBase
    {
        private readonly SheetListingViewModel _sheetListingViewModel;
        private readonly SelectedSheetStore _selectedSheetStore;
        private readonly ModuleStore _moduleStore;

        private readonly string _outputPath = @"..\..\..\PdfImages\";

        public DeleteSheetCommand(SheetListingViewModel sheetListingViewModel, SelectedSheetStore selectedSheetStore, ModuleStore moduleStore) 
        {
            _sheetListingViewModel = sheetListingViewModel;
            _selectedSheetStore = selectedSheetStore;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _sheetListingViewModel.IsDeleting = true;
            _sheetListingViewModel.ErrorMessage = string.Empty;

            try
            {
                if (_selectedSheetStore.SelectedSheet != null)
                {
                    await _moduleStore.DeleteSheet(_selectedSheetStore.SelectedSheet.Id);
                }                
            }
            catch (Exception)
            {
                _sheetListingViewModel.ErrorMessage = "Failed to delete sheet. Please try again later.";
            }
            finally
            {
                _sheetListingViewModel.IsDeleting = false;
            }
        }
    }
}
