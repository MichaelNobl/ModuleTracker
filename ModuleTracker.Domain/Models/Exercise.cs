using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Domain.Models
{
    public class Exercise
    {
        public Exercise(Guid id, Guid moduleId, Guid sheetId, int number, bool isCompleted = false)
        {
            Id = id;
            ModuleId = moduleId;
            SheetId = sheetId;
            Number = number;
            IsCompleted = isCompleted;
        }

        public Guid Id { get; }

        public Guid ModuleId { get; }

        public Guid SheetId { get; }
               
        public int Number { get; }

        public bool IsCompleted { get; }               

    }
}
