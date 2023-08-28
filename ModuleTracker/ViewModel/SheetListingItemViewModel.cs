using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class SheetListingItemViewModel : BaseViewModel
    {
        public SheetListingItemViewModel(Sheet sheet, ModalNavigationStore modalNavigationStore, ModuleStore moduleStore)
        {
            Sheet = sheet;
            OpenSheetCommand = new OpenExercisesCommand(sheet, modalNavigationStore, moduleStore);
            OpenPdfCommand = new OpenPdfViewCommand(this, modalNavigationStore, moduleStore);
            OnPropertyChanged(nameof(NumOfDoneExercises));
            OnPropertyChanged(nameof(NumOfExercises));
        }

        #region Properties

        public string NumOfDoneExercises => Sheet.Exercises.Where(x => x.IsCompleted).Count().ToString();

        public string NumOfExercises => Sheet.Exercises.Count.ToString();

        public Sheet Sheet { get; private set; }

        public string Name => $"Sheet {Sheet.SheetNumber}";

        public bool HasPdfFile => !string.IsNullOrEmpty(Sheet.PdfFilePath);

        #endregion

        #region Commands

        public ICommand OpenSheetCommand { get; private set; }
        public ICommand OpenPdfCommand { get; private set; }
        public ICommand CreatePdfCommand { get; private set; }

        #endregion

        #region Methods

        public void Update(Sheet sheet)
        {
            Sheet = sheet;
            OnPropertyChanged(nameof(NumOfExercises));
            OnPropertyChanged(nameof(NumOfDoneExercises));
            OnPropertyChanged(nameof(HasPdfFile));
        }

        #endregion
    }
}
