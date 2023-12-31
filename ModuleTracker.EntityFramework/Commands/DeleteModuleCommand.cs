﻿using Microsoft.EntityFrameworkCore;
using ModuleTracker.Domain.Commands;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework.Commands
{
    public class DeleteModuleCommand : IDeleteModuleCommand
    {
        private readonly ModulesDbContextFactory _contextFactory;

        public DeleteModuleCommand(ModulesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (var context = _contextFactory.Create())
            {
                var moduleDto = new ModuleDto()
                {
                    Id = id
                };

                context.Modules.Remove(moduleDto);

                var tempSheets = await context.Sheets.ToListAsync();

                var tempExercises = await context.Exercises.ToListAsync();

                foreach (var sheetDto in tempSheets)
                {
                    if(sheetDto.ModuleId == moduleDto.Id)
                    {   
                        context.Sheets.Remove(sheetDto);

                        foreach (var exerciseDto in tempExercises)
                        {
                            if(exerciseDto.ModuleId == moduleDto.Id && exerciseDto.SheetId == sheetDto.Id)
                            {
                                context.Exercises.Remove(exerciseDto);
                            }                            
                        }
                    }                    
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
