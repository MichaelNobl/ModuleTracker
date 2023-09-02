using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Models;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework.Commands
{
    public class CreateSheetCommand : ICreateSheetCommand
    {
        private readonly ModulesDbContextFactory _contextFactory;

        public CreateSheetCommand(ModulesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Sheet sheet)
        {            
            using (var context = _contextFactory.Create())
            {
                var sheetDto = new SheetDto()
                {
                    Id = sheet.Id,
                    ModuleId = sheet.ModuleId,
                    SheetNumber = sheet.SheetNumber,
                    Exercises = sheet.Exercises.Select(e => e.ToDto()).ToList(),
                    PdfFilePath = sheet.PdfFilePath
                };

                context.Sheets.Add(sheetDto);                               

                await context.SaveChangesAsync();
            }
        }
    }
}
