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

                var modules = modulesDtos.Select(m => new Module(m.Id, m.Name, new List<Sheet>())).ToList();

                var tempModules = modules.ToList();

                foreach (var module in tempModules) 
                {
                    var currentIndex = modules.FindIndex(m => m.Id == module.Id);

                    var sortetList = sheetsDtos.Where(s => s.ModuleId == module.Id).OrderBy(s => s.SheetNumber).ToList();

                    foreach (var sheetDto in sortetList)
                    {   
                        module.AddSheet(SheetDto.ToSheet(sheetDto));
                    }

                    modules[currentIndex] = module;                  

                }

                return modules;
            }
        }
    }
}
