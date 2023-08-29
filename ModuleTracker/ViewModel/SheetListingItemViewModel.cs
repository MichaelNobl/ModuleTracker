using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System.Linq;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class SheetListingItemViewModel : BaseViewModel
    {
        public SheetListingItemViewModel(Sheet sheet, ModalNavigationStore modalNavigationStore, ModuleStore moduleStore, SelectedModuleStore selectedModuleStore)
        {
            Sheet = sheet;
            OpenSheetCommand = new OpenExercisesCommand(sheet, modalNavigationStore, moduleStore);
            OpenPdfCommand = new OpenPdfCommand(this);
            AddPdfFileCommand = new UpdatePdfFilePathCommand(this, sheet, moduleStore, selectedModuleStore);

            OnPropertyChanged(nameof(NumOfDoneExercises));
            OnPropertyChanged(nameof(NumOfExercises));
        }

        #region Properties

        public string NumOfDoneExercises => Sheet.Exercises.Where(x => x.IsCompleted).Count().ToString();

        public string NumOfExercises => Sheet.Exercises.Count.ToString();

        public Sheet Sheet { get; private set; }

        public string Name => $"Sheet {Sheet.SheetNumber}";

        public bool HasPdfFile => !string.IsNullOrEmpty(Sheet.PdfFilePath);

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get
            {
                return _isSubmitting;
            }
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        #endregion

        #region Commands

        public ICommand OpenSheetCommand { get; private set; }
        public ICommand OpenPdfCommand { get; private set; }
        public ICommand AddPdfFileCommand { get; private set; }

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
