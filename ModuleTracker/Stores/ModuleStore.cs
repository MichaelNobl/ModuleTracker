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

        private readonly IGetAllModulesQuery _getAllModulesQuery;
        private readonly ICreateExerciseCommand _createExerciseCommand;
        private readonly ICreateModuleCommand _createModuleCommand;
        private readonly ICreateSheetCommand _createSheetCommand;
        private readonly IDeleteModuleCommand _deleteModuleCommand;
        private readonly IDeleteSheetCommand _deleteSheetCommand;
        private readonly IUpdateExerciseCommand _updateExerciseCommand;
        private readonly IUpdateSheetCommand _updateSheetCommand;

        public ModuleStore(
            IGetAllModulesQuery getAllModulesQuery,
            ICreateExerciseCommand createExerciseCommand,
            ICreateModuleCommand createModuleCommand,
            ICreateSheetCommand createSheetCommand, 
            IDeleteModuleCommand deleteModuleCommand,
            IDeleteSheetCommand deleteSheetCommand, 
            IUpdateExerciseCommand updateExerciseCommand, 
            IUpdateSheetCommand updateSheetCommand)
        {
            _getAllModulesQuery = getAllModulesQuery;
            _createExerciseCommand = createExerciseCommand;
            _createModuleCommand = createModuleCommand;
            _createSheetCommand = createSheetCommand;
            _deleteModuleCommand = deleteModuleCommand;
            _deleteSheetCommand = deleteSheetCommand;
            _updateExerciseCommand = updateExerciseCommand;
            _updateSheetCommand = updateSheetCommand;

            _modules = new List<Module>();
        }

        public async Task LoadModules()
        {
            var modules = await _getAllModulesQuery.Execute();

            _modules.Clear();
            _modules.AddRange(modules);

            ModulesLoaded?.Invoke();
        }

        public async Task AddModule(Module module)
        {
            await _createModuleCommand.Execute(module);

            _modules.Add(module);

            ModuleAdded?.Invoke(module);
        }
        public async Task DeleteModule(Guid id)
        {
            await _deleteModuleCommand.Execute(id);

            _modules.RemoveAll(m => m.Id == id);

            ModuleDeleted?.Invoke(id);
        }

        public async Task LoadSheets()
        {
            SheetsLoaded?.Invoke();
        }

        public async Task AddSheet(Sheet sheet)
        {
            await _createSheetCommand.Execute(sheet);

            _modules.SingleOrDefault(m => m.Id == sheet.ModuleId)?.AddSheet(sheet);

            SheetAdded?.Invoke(sheet);
        }

        public async Task UpdateSheet(Sheet sheet)
        {           
            await _updateSheetCommand.Execute(sheet);

            SheetUpdated?.Invoke(sheet);
        }

        public async Task DeleteSheet(Guid id)
        {
            await _deleteSheetCommand.Execute(id);

            foreach (var module in _modules)
            {               
               var sheet = _modules.SingleOrDefault(m => m.Id == module.Id)?.Sheets.SingleOrDefault(s => s.Id == id); 
               
                if(sheet is not null)
                {
                    _modules.SingleOrDefault(m => m.Id == module.Id)?.Sheets.Remove(sheet);
                }
            }            

            SheetDeleted?.Invoke(id);
        }

        public async Task LoadExercises()
        {
            ExerciseLoaded?.Invoke();
        }
        public async Task AddExercise(Exercise exercise)
        {
            await _createExerciseCommand.Execute(exercise);

            ExerciseAdded?.Invoke(exercise);
        }

        public async Task UpdateExercise(Exercise exercise)
        {
            await _updateExerciseCommand.Execute(exercise);

            var currentIndex = _modules.FindIndex(m => m.Id == exercise.ModuleId);

            var tempModule = _modules.SingleOrDefault(m => m.Id == exercise.ModuleId);

            if (tempModule is not null)
            {
                var module = new Module(exercise.ModuleId, tempModule.Name, new List<Sheet>());

                foreach (var sheet in tempModule.Sheets)
                {
                    var tempSheet = new Sheet(sheet.Id, sheet.ModuleId, sheet.SheetNumber, new List<Exercise>());

                    if (sheet.Id != exercise.SheetId)
                    {
                        module.AddSheet(sheet);
                    }
                    else
                    {
                        foreach (var exercises in sheet.Exercises)
                        {
                            if (exercises.Id != exercise.Id)
                            {
                                tempSheet.AddExercise(exercises);
                            }
                            else
                            {
                                tempSheet.AddExercise(exercise);
                            }
                        }                        

                        module.AddSheet(tempSheet);
                    }                    
                }

                _modules[currentIndex] = module;
            }

            ExerciseUpdated?.Invoke(exercise);
        }
    }
}
