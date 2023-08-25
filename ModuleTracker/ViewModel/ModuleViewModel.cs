using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleViewModel : BaseViewModel
    {       
        public ModuleViewModel(ModuleStore moduleStore, SelectedModuleStore selectedModuleStore, SelectedSheetStore selectedSheetStore, ModalNavigationStore modalNavigationStore)
        {
            ModulesListingViewModel = new ModuleListingViewModel(moduleStore, selectedModuleStore, selectedSheetStore, modalNavigationStore);
            SheetListingViewModel = new SheetListingViewModel(moduleStore, selectedModuleStore, selectedSheetStore, modalNavigationStore);

            LoadModuleCommand = new LoadModulesCommand(this, moduleStore);
            _errorMessage = string.Empty;
        }

        #region Properties

        public ModuleListingViewModel ModulesListingViewModel { get; }
        public SheetListingViewModel SheetListingViewModel { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
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

        #endregion

        #region Commands

        public ICommand LoadModuleCommand { get; }

        #endregion

        #region Methods
        public static ModuleViewModel LoadViewModel(ModuleStore moduleStore, SelectedModuleStore selectedModuleViewerStore, SelectedSheetStore selectedSheetStore, ModalNavigationStore modalNavigationStore)
        {
            var viewModel = new ModuleViewModel(moduleStore, selectedModuleViewerStore, selectedSheetStore, modalNavigationStore);

            viewModel.LoadModuleCommand.Execute(null);

            return viewModel;
        }

        #endregion

        
    }
}
