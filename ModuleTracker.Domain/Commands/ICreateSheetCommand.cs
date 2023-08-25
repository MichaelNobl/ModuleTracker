using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Queries
{
    public interface ICreateSheetCommand
    {
        Task Execute(Sheet sheet);
    }
}
