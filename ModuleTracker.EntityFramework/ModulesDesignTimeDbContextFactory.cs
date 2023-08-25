using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ModuleTracker.EntityFramework
{
    public class ModulesDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ModulesDbContext>
    {
        public ModulesDbContext CreateDbContext(string[] args = null)
        {
            return new ModulesDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=Modules.db").Options);
        }
    }
}
