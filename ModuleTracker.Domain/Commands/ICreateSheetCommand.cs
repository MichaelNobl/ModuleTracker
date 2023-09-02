using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Commands
{
    public interface ICreateSheetCommand
    {
        Task Execute(Sheet sheet);
    }
}
