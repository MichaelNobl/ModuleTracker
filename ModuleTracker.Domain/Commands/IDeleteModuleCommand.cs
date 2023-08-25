namespace ModuleTracker.Domain.Commands
{
    public interface IDeleteModuleCommand
    {
        Task Execute(Guid id);
    }
}
