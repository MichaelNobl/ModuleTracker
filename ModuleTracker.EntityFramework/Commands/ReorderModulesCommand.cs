using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Models;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework.Commands
{
    public class ReorderModulesCommand : IReorderModulesCommand
    {
        private readonly ModulesDbContextFactory _contextFactory;

        public ReorderModulesCommand(ModulesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Module selectedModule, Module targetedModule)
        {
            using (var context = _contextFactory.Create())
            {
                var moduleDto = new ModuleDto()
                {
                    Id = selectedModule.Id,
                    Name = selectedModule.Name,
                    Sheets = selectedModule.Sheets.Select(s => s.ToDto()).ToList(),
                    Order = targetedModule.Order,
                };

                context.Modules.Update(moduleDto);

                moduleDto = new ModuleDto()
                {
                    Id = targetedModule.Id,
                    Name = targetedModule.Name,
                    Sheets = targetedModule.Sheets.Select(s => s.ToDto()).ToList(),
                    Order = selectedModule.Order,
                };

                context.Modules.Update(moduleDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
