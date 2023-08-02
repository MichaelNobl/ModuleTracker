using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework
{
    public class ModuleDbContextFactory
    {
        private readonly DbContextOptions _options;

        public ModuleDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public ModuleDbContext Create()
        {
            return new ModuleDbContext(_options);
        }
    }
}
