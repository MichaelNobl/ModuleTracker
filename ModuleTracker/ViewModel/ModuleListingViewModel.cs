using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleListingViewModel : BaseViewModel
    {       
        private readonly ModuleStore _modulesStore;
        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly ObservableCollection<ModuleListingItemViewModel> _moduleListingItemViewModel;
        public IEnumerable<ModuleListingItemViewModel> ModuleListingItemViewModel =>
            _moduleListingItemViewModel;

        private ModuleListingItemViewModel _selectedModuleListingItemViewModel;

        public ModuleListingItemViewModel SelectedModuleListingItemViewModel
        {
            get
            {
                return _selectedModuleListingItemViewModel;
            }
            set
            {
                _selectedModuleListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedModuleListingItemViewModel));

                _selectedModuleStore.SelectedModule = _selectedModuleListingItemViewModel?.Module;
            }
        }

        public ICommand AddModuleCommand { get; set; }
        public ICommand DeleteModuleCommand { get; set; }

        public ModuleListingViewModel(ModuleStore modulStore, SelectedModuleStore selectedModuleStore, ModalNavigationStore modalNavigationStore)
        {
            _modulesStore = modulStore;
            _selectedModuleStore = selectedModuleStore;
            _modalNavigationStore = modalNavigationStore;
            _moduleListingItemViewModel = new ObservableCollection<ModuleListingItemViewModel>();          

            AddModuleCommand = new OpenAddModuleCommand(_modulesStore, modalNavigationStore);
            DeleteModuleCommand = new DeleteModuleCommand(_selectedModuleStore, _modulesStore);

            _modulesStore.ModulesLoaded += ModulesStoreModuleLoaded;
            _modulesStore.ModuleAdded += ModulesStoreModuleAdded;
            _modulesStore.ModuleDeleted += ModulesStoreModuleDeleted;

        }

        private void ModulesStoreModuleDeleted(Guid id)
        {
            var itemViewModel = _moduleListingItemViewModel.FirstOrDefault(m => m.Module.Id == id);

            if (itemViewModel != null)
            {
                _moduleListingItemViewModel.Remove(itemViewModel);
            }
        }

        private void ModulesStoreModuleLoaded()
        {
            _moduleListingItemViewModel.Clear();

            foreach (var module in _modulesStore.Modules)
            {
                AddModule(module);
            }
        }

        public override void Dispose()
        {

            _modulesStore.ModulesLoaded -= ModulesStoreModuleLoaded;
            _modulesStore.ModuleAdded -= ModulesStoreModuleAdded;
            _modulesStore.ModuleDeleted -= ModulesStoreModuleDeleted;

            base.Dispose();
        }

        private void ModulesStoreModuleAdded(Module module)
        {
            AddModule(module);
        }

        private void ModuleListingItemViewModelCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedModuleListingItemViewModel));
        }

        private void AddModule(Module module)
        {
            _moduleListingItemViewModel.Add(new ModuleListingItemViewModel(module));
        }

    }
}
