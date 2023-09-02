namespace ModuleTracker.Domain.Models
{
    public class Module
    {
        public Guid Id { get; }

        public string Name { get; }

        public IList<Sheet> Sheets { get; }
        
        public int Order { get; }
              
        public Module(Guid id, string name, IList<Sheet> sheet, int order)
        {
            Id = id;
            Name = name;
            Sheets = sheet;
            Order = order;
        }

        public void AddSheet(Sheet sheet)
        {
            Sheets.Add(sheet);
        }  
    }
}
