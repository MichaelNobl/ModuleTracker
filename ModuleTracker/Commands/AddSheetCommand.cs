using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class AddSheetCommand : AsyncCommandBase
    {
        private readonly AddSheetViewModel _addSheetViewModel;
        private readonly SheetStore _sheetStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedModuleStore _selectedModuleStore;
               

        public AddSheetCommand(AddSheetViewModel addSheetViewModel, SheetStore sheetStore, ModalNavigationStore modalNavigationStore, SelectedModuleStore selectedModuleStore)
        {
            _addSheetViewModel = addSheetViewModel;
            _sheetStore = sheetStore;
            _modalNavigationStore = modalNavigationStore;
            _selectedModuleStore = selectedModuleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var viewModel = _addSheetViewModel;

            var sheet = new Sheet(Guid.NewGuid(), int.Parse(viewModel.SheetNumber));

            for (var i = 1; i <= int.Parse(viewModel.NumOfExercises); i++)
            {
                var exercise = new Exercise(Guid.NewGuid(), i);
                sheet.AddExercise(exercise);
            }

            _selectedModuleStore.SelectedModule.AddSheet(sheet);            

            try
            {
                await _sheetStore.Add(sheet);

                foreach (var exercise in sheet.Exercises)
                {
                    await _sheetStore.AddExercise(exercise);
                }

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {

            }

            _modalNavigationStore.Close();
        }
    }
}
