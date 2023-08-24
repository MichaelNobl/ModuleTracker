using ModuleTracker.Domain.Models;
using ModuleTracker.Domain.Queries;
using ModuleTracker.EntityFramework.DTOs;

namespace ModuleTracker.EntityFramework.Commands
{
    public class CreateExerciseCommand : ICreateExerciseCommand
    {
        private readonly ModulesDbContextFactory _contextFactory;

        public CreateExerciseCommand(ModulesDbContextFactory contextFactory)
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

                context.Exercises.Add(exerciseDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
