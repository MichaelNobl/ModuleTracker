using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class EditSheetViewModel : BaseViewModel
    {
        private readonly ObservableCollection<EditSheetListingItemViewModel> _editSheetListingItemViewModel;
        public IEnumerable<EditSheetListingItemViewModel> EditSheetListingItemViewModel =>
            _editSheetListingItemViewModel;

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

        public EditSheetViewModel()
        {
            _editSheetListingItemViewModel = new ObservableCollection<EditSheetListingItemViewModel>();
            _sheetNumber = "0";
            _editSheetListingItemViewModel.Add(new EditSheetListingItemViewModel());
            _editSheetListingItemViewModel.Add(new EditSheetListingItemViewModel());
            _editSheetListingItemViewModel.Add(new EditSheetListingItemViewModel());
        }
    }
}
