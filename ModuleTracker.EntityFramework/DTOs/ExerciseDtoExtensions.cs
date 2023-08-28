using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework.DTOs
{
    public static class ExerciseDtoExtensions
    {
        public static ExerciseDto ToDto(this Exercise exercise)
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

        public static Exercise ToExercise(this ExerciseDto exerciseDto)
        {
            return new Exercise(exerciseDto.Id, exerciseDto.ModuleId, exerciseDto.SheetId, exerciseDto.Number, exerciseDto.IsCompleted);
        }
    }
}
