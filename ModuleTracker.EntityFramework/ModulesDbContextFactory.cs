using Microsoft.EntityFrameworkCore;

namespace ModuleTracker.EntityFramework
{
    public class ModulesDbContextFactory
    {
        private readonly DbContextOptions _options;

        public ModulesDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public ModulesDbContext Create()
        {
            return new ModulesDbContext(_options);
        }
    }
}
