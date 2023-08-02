using Microsoft.EntityFrameworkCore;
using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Models;
using ModuleTracker.EntityFramework.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework.Commands
{
    public class CreateModuleCommand : ICreateModuleCommand
    {
        private readonly ModuleDbContextFactory _contextFactory;

        public CreateModuleCommand(ModuleDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Module module)
        {
            var sheetDtos = new List<SheetDto>();

            foreach (var sheet in module.Sheets)
            {
                var sheetDto = new SheetDto()
                {
                    Id = sheet.Id,
                    ModuleId = sheet.ModuleId,
                    SheetNumber = sheet.SheetNumber,
                    NumOfExercises = sheet.NumOfExercises,
                };

                sheetDtos.Add(sheetDto);
            }

            using (var context = _contextFactory.Create())
            {
                var moduleDto = new ModuleDto() 
                {
                    Id = module.Id,
                    Name = module.Name,
                    Sheets = sheetDtos

                };

                context.Modules.Add(moduleDto);
                await context.SaveChangesAsync();

            }
        }
    }
}
