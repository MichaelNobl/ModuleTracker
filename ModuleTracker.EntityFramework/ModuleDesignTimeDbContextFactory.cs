using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework
{
    public class ModuleDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ModuleDbContext>
    {
        private readonly DbContextOptions _options;

        public ModuleDbContext CreateDbContext(string[] args = null)
        {
            return new ModuleDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=Modules.db").Options);
        }
    }
}
