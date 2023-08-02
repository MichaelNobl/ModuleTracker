using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
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
        public ICommand LoadYouTubeViewerCommand { get; }

        public ModuleViewModel(ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            ModulesListingViewModel = new ModuleListingViewModel();
            SheetListingViewModel = new SheetListingViewModel();

            AddModuleCommand = new OpenAddModuleCommand(moduleStore, modalNavigationStore);
            LoadYouTubeViewerCommand = new LoadModuleCommand(this, moduleStore);
        }

        public static ModuleViewModel LoadViewModel(ModuleStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            var viewModel = new ModuleViewModel(youTubeViewersStore, modalNavigationStore);

            viewModel.LoadYouTubeViewerCommand.Execute(null);

            return viewModel;
        }
    }
}
