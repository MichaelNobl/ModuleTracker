using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Stores
{
    public class SheetStore
    {
        private List<Sheet> _sheets;

        public IEnumerable<Sheet> Sheets => _sheets;

        public event Action SheetsLoaded;
        public event Action<Sheet> SheetAdded;
        public event Action<Sheet> SheetUpdated;
        public event Action<Guid> SheetDeleted;

        public SheetStore()
        {
            _sheets = new List<Sheet>();
        }

        public async Task Load()
        {
            SheetsLoaded?.Invoke();
        }

        public async Task Add(Sheet sheet)
        {
            SheetAdded?.Invoke(sheet);
        }

        public async Task Update(Sheet sheet)
        {

            SheetUpdated?.Invoke(sheet);
        }

        public async Task Delete(Guid id)
        {
            SheetDeleted?.Invoke(id);
        }
    }
}
