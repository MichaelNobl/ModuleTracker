using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Models;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework.Commands
{
    public class UpdateExerciseCommand : IUpdateExerciseCommand
    {
        private readonly ModulesDbContextFactory _contextFactory;

        public UpdateExerciseCommand(ModulesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Exercise exercise)
        {
            using (var context = _contextFactory.Create())
            {
                var exerciseDto = new ExerciseDto()
                {
                    Id = exercise.Id,
                    ModuleId = exercise.ModuleId,
                    SheetId = exercise.SheetId,
                    Number = exercise.Number,
                    IsCompleted = exercise.IsCompleted,
                };

                context.Exercises.Update(exerciseDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
