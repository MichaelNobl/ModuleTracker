namespace ModuleTracker.Domain.Models
{
    public class Sheet
    {
        public Guid Id { get; }

        public Guid ModuleId { get; }

        public int SheetNumber { get; }

        public IList<Exercise> Exercises { get; }

        public string PdfFilePath { get; private set; }

        public Sheet(Guid id, Guid moduleId, int sheetNumber, IList<Exercise> exercises, string pdfFilePath)
        {
            Id = id;
            ModuleId = moduleId;
            SheetNumber = sheetNumber;
            Exercises = exercises;
            PdfFilePath = pdfFilePath;
        }
        public void AddExercise(Exercise exercise)
        {
            Exercises.Add(exercise);
        }

        public void SetPdfFilePath(string pdfFilePath)
        {
            PdfFilePath = pdfFilePath;
        }
    }
}
