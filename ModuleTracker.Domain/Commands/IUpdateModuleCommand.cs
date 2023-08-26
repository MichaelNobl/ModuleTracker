using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Queries
{
    public interface IUpdateModuleCommand
    {
        Task Execute(Module module);
    }
}
