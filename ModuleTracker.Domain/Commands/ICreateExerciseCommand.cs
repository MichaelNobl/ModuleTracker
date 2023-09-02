using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Commands
{
    public interface ICreateExerciseCommand
    {
        Task Execute(Exercise exercise);
    }
}
