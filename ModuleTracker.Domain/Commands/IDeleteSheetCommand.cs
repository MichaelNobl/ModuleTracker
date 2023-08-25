namespace ModuleTracker.Domain.Commands
{
    public interface IDeleteSheetCommand
    {
        Task Execute(Guid id);
    }
}
