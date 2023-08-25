using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Queries
{
    public interface IUpdateSheetCommand
    {
        Task Execute(Sheet sheet);
    }
}
