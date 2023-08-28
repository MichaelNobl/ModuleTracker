using ModuleTracker.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ModuleTracker.EntityFramework.DTOs
{
    public class ExerciseDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ModuleId { get; set; }

        public Guid SheetId { get; set; }

        public int Number { get; set; }

        public bool IsCompleted { get; set; }       

    }
}
