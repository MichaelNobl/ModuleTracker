namespace ModuleTracker.Domain.Models
{
    public class Sheet
    {
        public Guid Id { get; }

        public Guid ModuleId { get; }

        public int SheetNumber { get; }

        public IList<Exercise> Exercises { get; }

        public Sheet(Guid id, Guid moduleId, int sheetNumber, IList<Exercise> exercises)
        {
            Id = id;
            ModuleId = moduleId;
            SheetNumber = sheetNumber;
            Exercises = exercises;
        }
        public void AddExercise(Exercise exercise)
        {
            Exercises.Add(exercise);
        }
    }
}
