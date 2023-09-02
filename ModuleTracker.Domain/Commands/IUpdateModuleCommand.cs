using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Commands
{
    public interface IUpdateModuleCommand
    {
        Task Execute(Module module);
    }
}
