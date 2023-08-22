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

        public IList<Exercise> Exercises { get; } = new List<Exercise>();

        public Sheet(Guid id, int sheetNumber)
        {
            Id = id;
            ModuleId = moduleID;
            SheetNumber = sheetNumber;
        }
        public void AddExercise(Exercise exercise)
        {
            Exercises.Add(exercise);
        }
    }
}
