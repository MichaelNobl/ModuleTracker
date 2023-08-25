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

        public IList<Sheet> Sheets { get; }
              
        public Module(Guid id, string name, IList<Sheet> sheet)
        {
            Id = id;
            Name = name;
            Sheets = sheet;
        }

        public void AddSheet(Sheet sheet)
        {
            Sheets.Add(sheet);
        }

        public void RemoveSheet(Sheet sheet)
        {
            Sheets.Remove(sheet);
        }
    }
}
