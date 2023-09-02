using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Models;
using ModuleTracker.EntityFramework.DTOs;

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
            using (var context = _contextFactory.Create())
            {
                var moduleDto = new ModuleDto()
                {
                    Id = module.Id,
                    Name = module.Name,
                    Sheets = module.Sheets.Select(s => s.ToDto()).ToList(),
                    Order = module.Order,
                };

                context.Modules.Add(moduleDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
