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
        private readonly ModuleDbContextFactory _contextFactory;

        public DeleteModuleCommand(ModuleDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (var context = _contextFactory.Create())
            {
                var youTubeViewerDto = new ModuleDto()
                {
                    Id = id
                };

                context.Modules.Remove(youTubeViewerDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
