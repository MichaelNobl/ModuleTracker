using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Queries
{
    public interface ICreateExerciseCommand
    {
        Task Execute(Exercise exercise);
    }
}
