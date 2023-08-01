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

        public int SheetNumber { get; }

        public int NumOfExercises { get; }

        public Sheet(Guid id, int sheetNumber, int numOfExercises)
        {
            Id = id;
            SheetNumber = sheetNumber;
            NumOfExercises = numOfExercises;
        }
    }
}
