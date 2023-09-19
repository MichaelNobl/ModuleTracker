using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class DeletePdfFilePathCommand : AsyncCommandBase
    {
        private ExercisesViewModel _exerciseItemViewModel;
        private Sheet _sheet;
        private ModuleStore _moduleStore;

        public DeletePdfFilePathCommand(ExercisesViewModel exerciseItemViewModel, Sheet sheet, ModuleStore moduleStore)
        {
            _exerciseItemViewModel = exerciseItemViewModel;
            _sheet = sheet;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _exerciseItemViewModel.ErrorMessage = string.Empty;            
            _exerciseItemViewModel.IsSubmitting = true;

            _sheet.SetPdfFilePath(string.Empty);
            _exerciseItemViewModel.Update(_sheet);

            try
            {
                await _moduleStore.UpdateSheet(_sheet);

            }
            catch (Exception)
            {
                _exerciseItemViewModel.ErrorMessage = "Failed to delete pdf file. Please try again later.";
            }
            finally
            {
                _exerciseItemViewModel.IsSubmitting = false;
            }
        }
    }
}
