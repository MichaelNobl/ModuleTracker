using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Domain.Models
{
    public class Exercise
    {
        public Exercise(Guid id, string moduleName, int sheetNumber, int number)
        {
            Id = id;
            ModuleName = moduleName;
            SheetNumber = sheetNumber;
            Number = number;
        }

        public Guid Id { get; }
               
        public string ModuleName { get; }

        public int SheetNumber { get; }

        public int Number { get; }

        public bool IsCompleted { get; }


    }
}
