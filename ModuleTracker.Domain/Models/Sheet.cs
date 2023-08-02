using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Domain.Models
{
    public class Sheet
    {
        public Guid Id { get; }

        public Guid ModuleId { get; }

        public int SheetNumber { get; }

        public int NumOfExercises { get; }

        public Sheet(Guid id, Guid moduleID, int sheetNumber, int numOfExercises)
        {
            Id = id;
            ModuleId = moduleID;
            SheetNumber = sheetNumber;
            NumOfExercises = numOfExercises;
        }
    }
}
