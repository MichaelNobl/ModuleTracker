using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Domain.Models
{
    public class Module
    {
        public Guid Id { get; }

        public string Name { get; }

        public IList<Sheet> Sheets { get; } = new List<Sheet>();
              
        public Module(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSheet(Sheet sheet)
        {
            Sheets.Add(sheet);
        }
    }
}
