using Aspose.Pdf;
using Microsoft.Win32;
using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class UpdatePdfFilePathCommand : AsyncCommandBase
    {
        private SheetListingItemViewModel _sheetListingItemViewModel;
        private Sheet _sheet;
        private ModuleStore _moduleStore;
        private SelectedModuleStore _selectedModuleStore;

        private readonly string _outputPath = @"..\..\..\PdfImages\";

        public UpdatePdfFilePathCommand(SheetListingItemViewModel sheetListingItemViewModel, Sheet sheet, ModuleStore moduleStore, SelectedModuleStore selectedModuleStore)
        {
            _sheetListingItemViewModel = sheetListingItemViewModel;
            _sheet = sheet;
            _moduleStore = moduleStore;
            _selectedModuleStore = selectedModuleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _sheetListingItemViewModel.ErrorMessage = string.Empty;
            _sheetListingItemViewModel.IsSubmitting = true;

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "pdf files (*.pdf) |*.pdf;";
            dlg.ShowDialog();

            _sheet.SetPdfFilePath(dlg.FileName);
            _sheetListingItemViewModel.Update(_sheet);

            try
            {
                await _moduleStore.UpdateSheet(_sheet);

            }
            catch (Exception)
            {
                _sheetListingItemViewModel.ErrorMessage = "Failed to update sheet. Please try again later.";
            }
            finally
            {
                _sheetListingItemViewModel.IsSubmitting = false;
            }
        }
    }
}
