using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ModuleTracker.Wpf.Stores
{
    public class SelectedSheetStore
    {
        private readonly ModuleStore _moduleStore;

        private Sheet _selectedSheet;

        public Sheet SelectedSheet
        {
            get
            {
                return _selectedSheet;
            }
            set
            {
                _selectedSheet = value;
                SelectedSheetChanged?.Invoke();
            }
        }

        public event Action SelectedSheetChanged;

        public SelectedSheetStore(ModuleStore moduleStore)
        {
            _moduleStore = moduleStore;

            _moduleStore.SheetAdded += ModuleStoreSheetAdded;
            _moduleStore.SheetDeleted += ModuleStoreSheetDeleted;
            _moduleStore.SheetUpdated += ModuleStoreSheetUpdated;
        }

        private void ModuleStoreSheetAdded(Sheet sheet)
        {
            SelectedSheet = sheet;
        }

        private void ModuleStoreSheetDeleted(Guid id)
        {
            if (id == SelectedSheet?.Id)
            {
                SelectedSheet = null;
            }
        }

        private void ModuleStoreSheetUpdated(Sheet sheet)
        {
            if (sheet.Id == SelectedSheet?.Id)
            {
                SelectedSheet = sheet;
            }
        }
    }
}
