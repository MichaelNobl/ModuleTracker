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

        public static ExerciseDto ToDto(Exercise exercise)
        {
            return new ExerciseDto()
            {
                Id = exercise.Id,
                ModuleId = exercise.ModuleId,
                SheetId = exercise.SheetId,
                Number = exercise.Number,
                IsCompleted = exercise.IsCompleted,
            };
        }

        public static Exercise ToExercise(ExerciseDto exerciseDto)
        {
            return new Exercise(exerciseDto.Id, exerciseDto.ModuleId, exerciseDto.SheetId, exerciseDto.Number, exerciseDto.IsCompleted);
        }

    }
}
