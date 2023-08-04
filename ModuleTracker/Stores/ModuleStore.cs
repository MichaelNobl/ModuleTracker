using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Stores
{
    public class ModuleStore
    {
        private List<Module> _modules;

        public IEnumerable<Module> Modules => _modules;
               
        public event Action ModulesLoaded;
        public event Action<Module> ModuleAdded;
        public event Action<Sheet> SheetAdded;
        public event Action<Guid> ModuleDeleted;

        public ModuleStore()
        {
            _modules = new List<Module>();
        }

        public async Task Load()
        {
            ModulesLoaded?.Invoke();
        }

        public async Task Add(Module module)
        {
            ModuleAdded?.Invoke(module);
        }

        public async Task AddSheet(Sheet sheet)
        {
            SheetAdded?.Invoke(sheet);
        }

        public async Task Delete(Guid id)
        {
            ModuleDeleted?.Invoke(id);
        }
    }
}
