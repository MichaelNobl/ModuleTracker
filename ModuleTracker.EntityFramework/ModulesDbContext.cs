using Microsoft.EntityFrameworkCore;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework
{
    public class ModulesDbContext : DbContext
    {
        public ModulesDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ModuleDto> Modules { get; set; } 

        public DbSet<SheetDto> Sheets { get; set; }

        public DbSet<ExerciseDto> Exercises { get; set; }
    }
}
