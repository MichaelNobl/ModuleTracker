using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class SheetListingViewModel : BaseViewModel
    {
        private readonly ObservableCollection<SheetListingItemViewModel> _sheetListingItemViewModel;

        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly SelectedSheetStore _selectedSheetStore;
        private ModuleStore _moduleStore;
        private ModalNavigationStore _modalNavigationStore;

        private Module SelectedModule => _selectedModuleStore.SelectedModule;
        private Sheet SelectedSheet => _selectedSheetStore.SelectedSheet;

        public bool HasSelectedModule => SelectedModule != null;
        public bool HasSelectedSheet => SelectedSheet != null;

        public IEnumerable<SheetListingItemViewModel> SheetListingItemViewModel =>
            _sheetListingItemViewModel;

        private SheetListingItemViewModel _selectedSheetListingItemViewModel;
        public SheetListingItemViewModel SelectedSheetListingItemViewModel
        {
            get
            {
                return _selectedSheetListingItemViewModel;
            }
            set
            {
                _selectedSheetListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedSheetListingItemViewModel));
            }
        }

        public ICommand AddSheetCommand { get; }
        public ICommand DeleteSheetCommand { get; }

        public SheetListingViewModel(ModuleStore moduleStore, SelectedModuleStore selectedModuleStore, SelectedSheetStore selectedSheetStore, ModalNavigationStore modalNavigationStore)
        {
            _selectedModuleStore = selectedModuleStore;
            _selectedSheetStore = selectedSheetStore;
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;

            _sheetListingItemViewModel = new ObservableCollection<SheetListingItemViewModel>();

            _selectedModuleStore.SelectedModuleChanged += SelectedModuleStoreSelectedModuleChanged;

            _moduleStore.SheetsLoaded += SheetStoreSheetLoaded;
            _moduleStore.SheetAdded += SheetStoreSheetAdded;
            _moduleStore.SheetDeleted += SheetStoreSheetDeleted;

            AddSheetCommand = new OpenAddSheetCommand(modalNavigationStore, moduleStore, _selectedModuleStore);
            DeleteSheetCommand = new DeleteSheetCommand(selectedSheetStore, moduleStore);
        }

        public override void Dispose()
        {
            _selectedModuleStore.SelectedModuleChanged -= SelectedModuleStoreSelectedModuleChanged;
            _moduleStore.SheetsLoaded -= SheetStoreSheetLoaded;
            _moduleStore.SheetAdded -= SheetStoreSheetAdded;
            _moduleStore.SheetDeleted -= SheetStoreSheetDeleted;

            base.Dispose();
        }

        private void SheetStoreSheetAdded(Sheet sheet)
        {
            _sheetListingItemViewModel.Add(new SheetListingItemViewModel(sheet, _modalNavigationStore, _moduleStore));
            OnPropertyChanged(nameof(SheetListingItemViewModel));
        }

        private void SheetStoreSheetDeleted(Guid id)
        {
            var itemViewModel = _sheetListingItemViewModel.FirstOrDefault(y => y.Sheet.Id == id);

            if (itemViewModel != null)
            {
                _sheetListingItemViewModel.Remove(itemViewModel);
            }
        }

        private void SheetStoreSheetLoaded()
        {
            _sheetListingItemViewModel.Clear();

            var module = _moduleStore.Modules.FirstOrDefault(m => m.Id == SelectedModule.Id);

            if(module != null)
            {
                foreach (var sheet in module.Sheets)
                {
                    AddSheet(sheet);
                }
            }
        }

        private void SelectedModuleStoreSelectedModuleChanged()
        {
            AddSheetItemViewModels();
            OnPropertyChanged(nameof(HasSelectedModule));
            OnPropertyChanged(nameof(SheetListingItemViewModel));
            OnPropertyChanged(nameof(SelectedSheetListingItemViewModel));
        }

        private void AddSheetItemViewModels()
        {
            _sheetListingItemViewModel.Clear();

            if (SelectedModule != null)
            {
                foreach (var sheet in SelectedModule.Sheets)
                {
                    _sheetListingItemViewModel.Add(new SheetListingItemViewModel(sheet, _modalNavigationStore, _moduleStore));
                }
            }
        }

        private void AddSheet(Sheet sheet)
        {
            var itemViewModel = new SheetListingItemViewModel(sheet, _modalNavigationStore, _moduleStore);
            _sheetListingItemViewModel.Add(itemViewModel);
        }
    }
}
