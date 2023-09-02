using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Commands
{
    public interface IUpdateSheetCommand
    {
        Task Execute(Sheet sheet);
    }
}
