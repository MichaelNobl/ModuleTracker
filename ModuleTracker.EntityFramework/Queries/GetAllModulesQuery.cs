using Microsoft.EntityFrameworkCore;
using ModuleTracker.Domain.Models;
using ModuleTracker.Domain.Queries;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework.Queries
{
    public class GetAllModulesQuery : IGetAllModulesQuery
    {
        private readonly ModulesDbContextFactory _contextFactory;

        public GetAllModulesQuery(ModulesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Module>> Execute()
        {
            using (var context = _contextFactory.Create())
            {
                var modulesDtos = await context.Modules.ToListAsync();
                var sheetsDtos = await context.Sheets.ToListAsync();
                var exerciseDtos = await context.Exercises.ToListAsync();

                var modules = modulesDtos.Select(m => new Module(m.Id, m.Name, m.Sheets.Select(s => SheetDto.ToSheet(s)).ToList())).ToList();

                var tempModules = modules.ToList();

                foreach (var module in tempModules) 
                {
                    var currentIndex = modules.FindIndex(m => m.Id == module.Id);

                    var ids = module.Sheets.Select(s => s.Id).ToList();

                    foreach (var sheetDto in sheetsDtos)
                    {                     
                        if(sheetDto.ModuleId == module.Id && !ids.Contains(sheetDto.Id))
                        {
                            module.AddSheet(SheetDto.ToSheet(sheetDto));
                        }                 
                    }

                    modules[currentIndex] = module;                  

                }

                return modules;
            }
        }
    }
}
