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
        private readonly ModuleStore _moudleStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedModuleStore _selectedModuleStore;
               

        public AddSheetCommand(AddSheetViewModel addSheetViewModel, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore, SelectedModuleStore selectedModuleStore)
        {
            _addSheetViewModel = addSheetViewModel;
            _moudleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;
            _selectedModuleStore = selectedModuleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var viewModel = _addSheetViewModel;
            viewModel.ErrorMessage = string.Empty;

            viewModel.IsSubmitting = true;

            var sheetNumber = int.Parse(viewModel.SheetNumber);
            var sheetNumbers = _selectedModuleStore.SelectedModule.Sheets.Select(s => s.SheetNumber);


            if (sheetNumber < 0)
            {
                viewModel.ErrorMessage = "Sheet number must be a non-negative integer";
                viewModel.IsSubmitting = false;
                return;
            }
            else if(int.Parse(viewModel.NumOfExercises) < 1)
            {
                viewModel.ErrorMessage = "Number of exercises must be greater than 1";
                viewModel.IsSubmitting = false;
                return;
            }
            else if(sheetNumbers.Contains(sheetNumber))
            {
                viewModel.ErrorMessage = "This sheet already exists.";
                viewModel.IsSubmitting = false;
                return;
            }


            var sheet = new Sheet(Guid.NewGuid(), _selectedModuleStore.SelectedModule.Id, sheetNumber, new List<Exercise>());

            for (var i = 1; i <= int.Parse(viewModel.NumOfExercises); i++)
            {
                var exercise = new Exercise(Guid.NewGuid(), sheet.ModuleId, sheet.Id, i);
                sheet.AddExercise(exercise);
            }        

            try
            {
                await _moudleStore.AddSheet(sheet);

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
    }
}
