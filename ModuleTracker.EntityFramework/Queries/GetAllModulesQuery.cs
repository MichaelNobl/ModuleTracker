using Microsoft.EntityFrameworkCore;
using ModuleTracker.Domain.Models;
using ModuleTracker.Domain.Queries;
using ModuleTracker.EntityFramework.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework.Queries
{
    public class GetAllModulesQuery : IGetAllModulesQuery
    {
        private readonly ModuleDbContextFactory _contextFactory;

        public GetAllModulesQuery(ModuleDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Module>> Execute()
        {           
            using (var context = _contextFactory.Create())
            {
                var moduleDtos = await context.Modules.ToListAsync();

                var modules = new List<Module>();

                foreach (var module in moduleDtos)
                {
                    var sheets = new List<Sheet>();

                    foreach (var sheetDtos in module.Sheets)
                    {
                        var sheet = new Sheet(sheetDtos.Id, module.Id, sheetDtos.SheetNumber, sheetDtos.NumOfExercises);

                        sheets.Add(sheet);
                    }

                    modules.Add(new Module(module.Id, module.Name, sheets));
                }                

                return modules;
            }
        }
    }
}
