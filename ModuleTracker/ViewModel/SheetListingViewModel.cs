using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
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
        public ICommand EditSheetCommand { get; }

        public SheetListingViewModel(SelectedModuleStore selectedModuleStore, SheetStore sheetStore, ModalNavigationStore modalNavigationStore)
        {
            _selectedModuleStore = selectedModuleStore;

            _sheetListingItemViewModel = new ObservableCollection<SheetListingItemViewModel>();

            _selectedModuleStore.SelectedModuleChanged += SelectedModuleStoreSelectedModuleChanged;

            AddSheetCommand = new OpenAddSheetCommand(modalNavigationStore, sheetStore, _selectedModuleStore);

            sheetStore.SheetAdded += SheetStoreSheetAdded;
        }

        private void SheetStoreSheetAdded(Sheet sheet)
        {
            _sheetListingItemViewModel.Add(new SheetListingItemViewModel(sheet));
            OnPropertyChanged(nameof(SheetListingItemViewModel));
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

            if(SelectedModule != null)
            {
                foreach (var sheet in SelectedModule.Sheets)
                {
                    _sheetListingItemViewModel.Add(new SheetListingItemViewModel(sheet));
                }
            }            
        }
    }
}
