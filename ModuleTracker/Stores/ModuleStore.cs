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
        private List<Module> _modules;

        public IEnumerable<Module> Modules => _modules;
               
        public event Action ModulesLoaded;
        public event Action<Module> ModuleAdded;
        public event Action<Guid> ModuleDeleted;
        public event Action SheetsLoaded;
        public event Action<Sheet> SheetAdded;
        public event Action<Sheet> SheetUpdated;
        public event Action<Guid> SheetDeleted;
        public event Action ExerciseLoaded;
        public event Action<Exercise> ExerciseAdded;
        public event Action<Exercise> ExerciseUpdated;

        public ModuleStore()
        {
            _getAllModulesQuery = getAllModulesQuery;
            _createModuleCommand = createModuleCommand;
            _updateModuleCommand = updateModuleCommand;
            _deleteModuleCommand = deleteModuleCommand;

            _modules = new List<Module>();
        }

        public async Task LoadModules()
        {
            ModulesLoaded?.Invoke();
        }

        public async Task AddModule(Module module)
        {
            ModuleAdded?.Invoke(module);
        }
        public async Task DeleteModule(Guid id)
        {
            ModuleDeleted?.Invoke(id);
        }

        public async Task LoadSheets()
        {
            SheetsLoaded?.Invoke();
        }

        public async Task AddSheet(Sheet sheet)
        {
            SheetAdded?.Invoke(sheet);
        }

        public async Task UpdateSheet(Sheet sheet)
        {
            SheetUpdated?.Invoke(sheet);
        }

        public async Task DeleteSheet(Guid id)
        {
            SheetDeleted?.Invoke(id);
        }

        public async Task LoadExercises()
        {
            ExerciseLoaded?.Invoke();
        }
        public async Task AddExercise(Exercise exercise)
        {
            ExerciseAdded?.Invoke(exercise);
        }

        public async Task UpdateExercise(Exercise exercise)
        {
            ExerciseUpdated?.Invoke(exercise);
        }
    }
}
