﻿using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class AddSheetViewModel : BaseViewModel
    {       
        public AddSheetViewModel(ModuleStore moduleStore, ModalNavigationStore modalNavigationStore, SelectedModuleStore selectedModuleStore)
        {
            SubmitCommand = new AddSheetCommand(this, moduleStore, modalNavigationStore, selectedModuleStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
            PdfFilePathCommand = new AddPdfFileCommand(this);
            _pdfFilePath = string.Empty;
        }

        #region Properties

        private string _numOfExercises;
        public string NumOfExercises
        {
            get
            {
                return _numOfExercises;
            }
            set
            {
                _numOfExercises = value;
                OnPropertyChanged(nameof(NumOfExercises));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

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
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _pdfFilePath;
        public string PdfFilePath
        {
            get
            {
                return _pdfFilePath;
            }
            set
            {
                _pdfFilePath = value;
                OnPropertyChanged(nameof(PdfFilePath));
                OnPropertyChanged(nameof(HasPdfPath));
            }
        }

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

        public bool CanSubmit => !string.IsNullOrEmpty(NumOfExercises) && !string.IsNullOrEmpty(SheetNumber);
        public bool HasPdfPath => !string.IsNullOrEmpty(PdfFilePath);

        #endregion

        #region Commands
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand PdfFilePathCommand { get; set; }

        #endregion

    }
}
