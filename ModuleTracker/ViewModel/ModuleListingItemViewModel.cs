using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleListingItemViewModel : BaseViewModel
    {
        public Module Module { get; private set; }

        public string Name => Module.Name;

        public ModuleListingItemViewModel(Module module)
        {
            Module = module;
        }
    }
}
