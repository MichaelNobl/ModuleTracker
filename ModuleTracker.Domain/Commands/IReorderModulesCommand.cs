using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Commands
{
    public interface IReorderModulesCommand
    {
        Task Execute(Module selectedModule, Module targetedModule);
    }
}
