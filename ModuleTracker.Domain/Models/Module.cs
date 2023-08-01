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

        public Module(Guid id, string name, IList<Sheet> sheets)
        {
            Id = id;
            Name = name;
            Sheets = sheets;
        }
    }
}
