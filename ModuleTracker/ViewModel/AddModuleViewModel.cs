using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class AddModuleViewModel : BaseViewModel
    {
        public AddModuleViewModel(ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            var submitCommand = new AddModuleCommand(this, moduleStore, modalNavigationStore);
            var cancelCommand = new CloseModalCommand(modalNavigationStore);
        }

    }
}
