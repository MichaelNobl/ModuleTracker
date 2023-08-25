using ModuleTracker.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework.DTOs
{
    public class SheetDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ModuleId { get; set; }

        public int SheetNumber { get; set; }

        public IList<ExerciseDto> Exercises { get; set; } = new List<ExerciseDto>();

        public static SheetDto ToDto(Sheet sheet)
        {
            return new SheetDto()
            {
                Id = sheet.Id,
                ModuleId = sheet.ModuleId,
                SheetNumber = sheet.SheetNumber,
                Exercises = sheet.Exercises.Select(e => ExerciseDto.ToDto(e)).ToList()
            };
        }

        public static Sheet ToSheet(SheetDto sheetDto)
        {
            return new Sheet(sheetDto.Id, sheetDto.ModuleId, sheetDto.SheetNumber, sheetDto.Exercises.Select(e => ExerciseDto.ToExercise(e)).OrderBy(e => e.Number).ToList());
        }
    }
}
