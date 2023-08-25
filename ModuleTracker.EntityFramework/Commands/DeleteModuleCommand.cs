using ModuleTracker.Domain.Commands;
using ModuleTracker.EntityFramework.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //implement errormessage

                var moduleDto = new ModuleDto()
                {
                    Id = id
                };

                context.Modules.Remove(moduleDto);

                var tempSheets = context.Sheets.ToList();

                var tempExercises = context.Exercises.ToList();

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
