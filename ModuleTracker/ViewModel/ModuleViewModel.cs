using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleViewModel : BaseViewModel
    {
        public ModuleListingViewModel ModulesListingViewModel { get; }
        public SheetListingViewModel SheetListingViewModel { get; }

        public ICommand AddModuleCommand { get; }

        public ModuleViewModel()
        {
            ModulesListingViewModel = new ModuleListingViewModel();
            SheetListingViewModel = new SheetListingViewModel();
        }

    }
}
