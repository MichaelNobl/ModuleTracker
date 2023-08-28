using ModuleTracker.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ModuleTracker.EntityFramework.DTOs
{
    public class SheetDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ModuleId { get; set; }

        public int SheetNumber { get; set; }

        public IList<ExerciseDto> Exercises { get; set; } = new List<ExerciseDto>();

        public string PdfFilePath { get; set; }
    }
}
