using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleListingViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ModuleListingItemViewModel> _moduleListingItemViewModel;

        public IEnumerable<ModuleListingItemViewModel> ModuleListingItemViewModel =>
            _moduleListingItemViewModel;

        public ModuleListingViewModel()
        {
            _moduleListingItemViewModel = new ObservableCollection<ModuleListingItemViewModel>();

            _moduleListingItemViewModel.Add(new ModuleListingItemViewModel(new Module(new Guid(), "Höhere Analysis", new List<Sheet>())));
        }

        public string NumOfSheets => ModuleListingItemViewModel.Count().ToString();

    }
}
