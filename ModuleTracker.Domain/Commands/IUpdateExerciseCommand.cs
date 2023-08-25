using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Commands
{
    public interface IUpdateExerciseCommand
    {
        Task Execute(Exercise exercise);
    }
}
