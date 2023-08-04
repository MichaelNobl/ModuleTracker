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

        public string ModuleName { get; }

        public int SheetNumber { get; }

        public IList<Exercise> Exercises { get; } = new List<Exercise>();

        public Sheet(Guid id, string moduleName, int sheetNumber, int numOfExercises )
        {
            Id = id;
            ModuleName = moduleName;
            SheetNumber = sheetNumber;
            CreateExercises(numOfExercises);
        }

        public void CreateExercises(int numOfExercises)
        {
            for(int i = 0; i< numOfExercises; i++)
            {
                Exercises.Add(new Exercise(new Guid(), ModuleName, SheetNumber, i));
            }
        }
    }
}
