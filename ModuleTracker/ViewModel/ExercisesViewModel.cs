using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ExercisesViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ExerciseListingItemViewModel> _exerciseListingItemViewModel;
        private readonly ModuleStore _moduleStore;               

        public ExercisesViewModel(Sheet sheet, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {            
            _sheetNumber = sheet.SheetNumber.ToString();
            _errorMessage = string.Empty;

            Sheet = sheet;
            _exerciseListingItemViewModel = new ObservableCollection<ExerciseListingItemViewModel>();
            _moduleStore = moduleStore;

            OkCommand = new OkExercisesCommand(this, moduleStore, modalNavigationStore);
            OpenPdfCommand = new OpenPdfCommand(this); 
            ChangePdfPathCommand = new UpdatePdfFilePathCommand(this, sheet, moduleStore); 
            DeletePdfPathCommand = new DeletePdfFilePathCommand(this, sheet, moduleStore); 

            AddExerciseItemViewModels();           

            _moduleStore.ExerciseUpdated += SheetStoreExerciseUpdated;
        }

        #region Properties

        public Sheet Sheet { get; set; }

        private string _sheetNumber;
        public string SheetNumber
        {
            get
            {
                return _sheetNumber;
            }
            set
            {
                _sheetNumber = value;
                OnPropertyChanged(nameof(SheetNumber));
            }
        }

        public IEnumerable<ExerciseListingItemViewModel> ExerciseListingItemViewModel =>
            _exerciseListingItemViewModel;

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

        public bool HasPdfFile => !string.IsNullOrEmpty(Sheet.PdfFilePath);

        #endregion

        #region Commands

        public ICommand OkCommand { get; set; }
        public ICommand OpenPdfCommand { get; set; }
        public ICommand ChangePdfPathCommand { get; set; }
        public ICommand DeletePdfPathCommand { get; set; }

        #endregion

        #region Actions

        public override void Dispose()
        {
            _moduleStore.ExerciseUpdated -= SheetStoreExerciseUpdated;

            base.Dispose();
        }

        private void SheetStoreExerciseUpdated(Exercise exercise)
        {
            var exerciseModel = _exerciseListingItemViewModel.FirstOrDefault(e => e.ExerciseId == exercise.Id);

            exerciseModel?.Update(exercise);
        }

        #endregion

        #region Methods
        private void AddExerciseItemViewModels()
        {
            _exerciseListingItemViewModel.Clear();

            var sheet = _moduleStore.Modules.SingleOrDefault(m => m.Id == Sheet.ModuleId)?.Sheets.SingleOrDefault(s => s.Id == Sheet.Id);

            if (sheet != null)
            {
                foreach (var exercise in sheet.Exercises)
                {
                    _exerciseListingItemViewModel.Add(new ExerciseListingItemViewModel(exercise));
                }
            }
        }
        public void Update(Sheet sheet)
        {
            Sheet = sheet;
            OnPropertyChanged(nameof(HasPdfFile));
        }

        #endregion
    }
}
