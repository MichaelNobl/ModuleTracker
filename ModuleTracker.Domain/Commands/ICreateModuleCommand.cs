using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Commands
{
    public interface ICreateModuleCommand
    {
        Task Execute(Module module);
    }
}
