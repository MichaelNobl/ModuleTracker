using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Models;
using ModuleTracker.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Stores
{
    public class ModuleStore
    {
        private readonly IGetAllModulesQuery _getAllModulesQuery;
        private readonly ICreateModuleCommand _createModuleCommand;
        private readonly IUpdateModuleCommand _updateModuleCommand;
        private readonly IDeleteModuleCommand _deleteModuleCommand;
        private List<Module> _modules;

        public IEnumerable<Module> Modules => _modules;

        public event Action ModuleLoaded;
        public event Action<Module> ModuleAdded;
        public event Action<Guid> ModuleDeleted;

        public ModuleStore(
            IGetAllModulesQuery getAllModulesQuery,
            ICreateModuleCommand createModuleCommand,
            IUpdateModuleCommand updateModuleCommand,
            IDeleteModuleCommand deleteModuleCommand)
        {
            _getAllModulesQuery = getAllModulesQuery;
            _createModuleCommand = createModuleCommand;
            _updateModuleCommand = updateModuleCommand;
            _deleteModuleCommand = deleteModuleCommand;

            _modules = new List<Module>();
        }

        public async Task Load()
        {
            var modules = await _getAllModulesQuery.Execute();

            _modules.Clear();
            _modules.AddRange(modules);

            ModuleLoaded?.Invoke();
        }

        public async Task Add(Module module)
        {
            await _createModuleCommand.Execute(module);

            _modules.Add(module);

            ModuleAdded?.Invoke(module);                        
        }

        public async Task Delete(Guid id)
        {
            await _deleteModuleCommand.Execute(id);

            _modules.RemoveAll(y => y.Id == id);

            ModuleDeleted?.Invoke(id);
        }
    }
}
