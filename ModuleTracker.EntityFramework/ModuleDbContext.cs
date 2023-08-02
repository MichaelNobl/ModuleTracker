using Microsoft.EntityFrameworkCore;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework
{
    public class ModuleDbContext : DbContext
    {
        public ModuleDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ModuleDto> Modules { get; set; }
    }
}
