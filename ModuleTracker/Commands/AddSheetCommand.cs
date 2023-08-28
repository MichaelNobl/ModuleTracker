using Aspose.Pdf;
using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class AddSheetCommand : AsyncCommandBase
    {
        private readonly AddSheetViewModel _addSheetViewModel;
        private readonly ModuleStore _moduleStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly string _outputPath = @"..\..\..\PdfImages\";

        public AddSheetCommand(AddSheetViewModel addSheetViewModel, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore, SelectedModuleStore selectedModuleStore)
        {
            _addSheetViewModel = addSheetViewModel;
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;
            _selectedModuleStore = selectedModuleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var viewModel = _addSheetViewModel;
            viewModel.ErrorMessage = string.Empty;

            viewModel.IsSubmitting = true;

            int sheetNumber;
            int numOfExercises;

            try
            {
                sheetNumber = int.Parse(viewModel.SheetNumber);
                numOfExercises = int.Parse(viewModel.NumOfExercises);
            }
            catch (Exception)
            {
                viewModel.ErrorMessage = "Sheet number or number of exercises is not an integer!";
                viewModel.IsSubmitting = false;
                return;
            }
            
            var sheetNumbers = _selectedModuleStore.SelectedModule.Sheets.Select(s => s.SheetNumber);

            if (sheetNumber < 0)
            {
                viewModel.ErrorMessage = "Sheet number must be a non-negative integer";
                viewModel.IsSubmitting = false;
                return;
            }
            else if(int.Parse(viewModel.NumOfExercises) < 1)
            {
                viewModel.ErrorMessage = "Number of exercises must be greater than 0";
                viewModel.IsSubmitting = false;
                return;
            }
            else if(sheetNumbers.Contains(sheetNumber))
            {
                viewModel.ErrorMessage = "This sheet already exists.";
                viewModel.IsSubmitting = false;
                return;
            }

            if (!string.IsNullOrEmpty(_addSheetViewModel.PdfFilePath))
            {
                SavePdfFileAsImage();
            }

            var sheet = new Sheet(Guid.NewGuid(), _selectedModuleStore.SelectedModule.Id, sheetNumber, new List<Exercise>(), _addSheetViewModel.PdfFilePath);

            for (var i = 1; i <= numOfExercises; i++)
            {
                var exercise = new Exercise(Guid.NewGuid(), sheet.ModuleId, sheet.Id, i);
                sheet.AddExercise(exercise);
            }        

            try
            {
                await _moduleStore.AddSheet(sheet);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                viewModel.ErrorMessage = "Failed to add sheet. Please try again later.";
            }
            finally
            {
                viewModel.IsSubmitting = false;
            }
        }

        private void SavePdfFileAsImage()
        {
            var pdfFilePath = _addSheetViewModel.PdfFilePath;
            var pdfName = pdfFilePath.Substring(pdfFilePath.LastIndexOf("\\") + 1, pdfFilePath.Length - pdfFilePath.LastIndexOf("\\") - 5);

            // load PDF with an instance of Document                        
            var document = new Document(pdfFilePath);

            // create an object of EmfDevice
            var renderer = new Aspose.Pdf.Devices.PngDevice();

            var moduleName = _moduleStore.Modules.SingleOrDefault(m => m.Id == _selectedModuleStore.SelectedModule.Id)?.Name;

            var outputPath = $"{_outputPath}{moduleName}_{_addSheetViewModel.SheetNumber}_{pdfName}.png";

            renderer.Process(document.Pages[1], outputPath);
        }
    }
}
