using Microsoft.EntityFrameworkCore;
using ModuleTracker.Domain.Commands;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework.Commands
{
    public class DeleteSheetCommand : IDeleteSheetCommand
    {
        private readonly ModulesDbContextFactory _contextFactory;

        public DeleteSheetCommand(ModulesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (var context = _contextFactory.Create())
            {
                var sheetDto = new SheetDto()
                {
                    Id = id
                };

                context.Sheets.Remove(sheetDto);

                var exercises = await context.Exercises.ToListAsync();

                foreach (var exerciseDto in exercises)
                {
                    if(exerciseDto.SheetId == id)
                    {
                        context.Exercises.Remove(exerciseDto);
                    }                   
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
