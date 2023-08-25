using Microsoft.EntityFrameworkCore;
using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Models;
using ModuleTracker.Domain.Queries;
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
        private readonly ModulesDbContextFactory _contextFactory;

        public CreateModuleCommand(ModulesDbContextFactory contextFactory)
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
                    Sheets = module.Sheets.Select(s => SheetDto.ToDto(s)).ToList(),
                };

                context.Modules.Add(moduleDto);
                await context.SaveChangesAsync();

            }
        }
    }
}
