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
        }
        public ModuleListingViewModel ModulesListingViewModel { get; }
        public SheetListingViewModel SheetListingViewModel { get; }

        public ICommand LoadModuleCommand { get; }

        public static ModuleViewModel LoadViewModel(ModuleStore moduleStore, SelectedModuleStore selectedModuleViewerStore, SelectedSheetStore selectedSheetStore, ModalNavigationStore modalNavigationStore)
        {
            var viewModel = new ModuleViewModel(moduleStore, selectedModuleViewerStore, selectedSheetStore, modalNavigationStore);

            viewModel.LoadModuleCommand.Execute(null);

            return viewModel;
        }

        #region Properties

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

        #endregion
    }
}
