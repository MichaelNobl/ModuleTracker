using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Queries
{
    public interface ICreateModuleCommand
    {
        Task Execute(Module module);
    }
}
