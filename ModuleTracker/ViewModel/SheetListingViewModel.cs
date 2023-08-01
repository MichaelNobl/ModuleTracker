using ModuleTracker.Domain.Models;
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

        public ICommand EditSheetCommand { get; }

        public SheetListingViewModel()
        {
            _sheetListingItemViewModel = new ObservableCollection<SheetListingItemViewModel>();

            _sheetListingItemViewModel.Add(new SheetListingItemViewModel(new Sheet(new Guid(), 1, 5)));
            _sheetListingItemViewModel.Add(new SheetListingItemViewModel(new Sheet(new Guid(), 2, 5)));
            _sheetListingItemViewModel.Add(new SheetListingItemViewModel(new Sheet(new Guid(), 3, 5)));
        }

    }
}
