using ModuleTracker.Domain.Models;

namespace ModuleTracker.Domain.Queries
{
    public interface IGetAllModulesQuery
    {
        Task<IEnumerable<Module>> Execute();
    }
}
