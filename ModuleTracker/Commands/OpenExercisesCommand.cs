using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.Stores.ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class OpenExercisesCommand : CommandBase
    {
        private readonly Sheet _sheet;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SheetStore _sheetStore;
        private readonly SelectedSheetStore _selectedSheetStore;

        public OpenExercisesCommand(Sheet sheet, ModalNavigationStore modalNavigationStore, SheetStore sheetStore, SelectedSheetStore selectedSheetStore)
        {
            _sheet = sheet;
            _modalNavigationStore = modalNavigationStore;
            _sheetStore = sheetStore;
            _selectedSheetStore = selectedSheetStore;
        }

        public override void Execute(object? parameter)
        {
            var exerciseListingViewModel = new ExercisesViewModel(_sheet, _sheetStore, _modalNavigationStore, _selectedSheetStore);
            _modalNavigationStore.CurrentViewModel = exerciseListingViewModel;
        }
    }
}
