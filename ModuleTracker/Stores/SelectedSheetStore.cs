using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Stores
{
    using global::ModuleTracker.Domain.Models;
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
            private readonly SheetStore _sheetStore;

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

            public SelectedSheetStore(SheetStore sheetStore)
            {
                _sheetStore = sheetStore;

                _sheetStore.SheetUpdated += SheetStoreSheetUpdated;
                _sheetStore.SheetAdded += SheetStoreSheetAdded;
                _sheetStore.SheetDeleted += SheetStoreSheetDeleted;
            }

            private void SheetStoreSheetAdded(Sheet sheet)
            {
                SelectedSheet = sheet;
            }

            private void SheetStoreSheetDeleted(Guid id)
            {
                if (id == SelectedSheet?.Id)
                {
                    SelectedSheet = null;
                }
            }

            private void SheetStoreSheetUpdated(Sheet sheet)
            {
                if (sheet.Id == SelectedSheet?.Id)
                {
                    SelectedSheet = sheet;
                }
            }
        }
    }

}
