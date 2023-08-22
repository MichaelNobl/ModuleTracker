using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.Stores.ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class SheetListingViewModel : BaseViewModel
    {
        private readonly ObservableCollection<SheetListingItemViewModel> _sheetListingItemViewModel;

        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly SelectedSheetStore _selectedSheetStore;
        private SheetStore _sheetStore;
        private ModalNavigationStore _modalNavigationStore;

        private Module SelectedModule => _selectedModuleStore.SelectedModule;

        public bool HasSelectedModule => SelectedModule != null;

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

        public SheetListingViewModel(SelectedModuleStore selectedModuleStore, SelectedSheetStore selectedSheetStore, SheetStore sheetStore, ModalNavigationStore modalNavigationStore)
        {
            _selectedModuleStore = selectedModuleStore;
            _selectedSheetStore = selectedSheetStore;
            _sheetStore = sheetStore;
            _modalNavigationStore = modalNavigationStore;

            _sheetListingItemViewModel = new ObservableCollection<SheetListingItemViewModel>();

            _selectedModuleStore.SelectedModuleChanged += SelectedModuleStoreSelectedModuleChanged;

            _sheetStore.SheetsLoaded += SheetStoreSheetLoaded;
            _sheetStore.SheetAdded += SheetStoreSheetAdded;
            _sheetStore.SheetDeleted += SheetStoreSheetDeleted;

            AddSheetCommand = new OpenAddSheetCommand(modalNavigationStore, sheetStore, _selectedModuleStore);           
        }

        public override void Dispose()
        {
            _selectedModuleStore.SelectedModuleChanged -= SelectedModuleStoreSelectedModuleChanged;
            _sheetStore.SheetsLoaded -= SheetStoreSheetLoaded;
            _sheetStore.SheetAdded -= SheetStoreSheetAdded;
            _sheetStore.SheetDeleted -= SheetStoreSheetDeleted;

            base.Dispose();
        }

        private void SheetStoreSheetAdded(Sheet sheet)
        {
            _sheetListingItemViewModel.Add(new SheetListingItemViewModel(sheet, _modalNavigationStore, _sheetStore, _selectedSheetStore));
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

            foreach (var sheet in _sheetStore.Sheets)
            {
                AddSheet(sheet);
            }
        }

        private void SelectedModuleStoreSelectedModuleChanged()
        {
            OnPropertyChanged(nameof(HasSelectedModule));
            OnPropertyChanged(nameof(SheetListingItemViewModel));
            OnPropertyChanged(nameof(SelectedSheetListingItemViewModel));
        }

        private void AddSheet(Sheet sheet)
        {
            var itemViewModel = new SheetListingItemViewModel(sheet, _modalNavigationStore, _sheetStore, _selectedSheetStore);
            _sheetListingItemViewModel.Add(itemViewModel);
        }
    }
}
