using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class SheetItemInsertetCommand : CommandBase
    {
        private readonly SheetListingViewModel _listingViewModel;

        public SheetItemInsertetCommand(SheetListingViewModel sheetListingViewModel)
        {
            _listingViewModel = sheetListingViewModel;
        }

        public override void Execute(object? parameter)
        {
            _listingViewModel.InsertSheet(_listingViewModel.InsertetSheetItemViewModel, _listingViewModel.TargetetSheetItemViewModel);
        }
    }
}
