using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class ModuleItemInsertetCommand : CommandBase
    {
        private readonly ModuleListingViewModel _listingViewModel;

        public ModuleItemInsertetCommand(ModuleListingViewModel moduleListingViewModel)
        {
            _listingViewModel = moduleListingViewModel;
        }

        public override void Execute(object? parameter)
        {
            _listingViewModel.InsertModule(_listingViewModel.InsertetModuleItemViewModel, _listingViewModel.TargetetModuleItemViewModel);
        }
    }
}
