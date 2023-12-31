﻿using ModuleTracker.Domain.Models;
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
        private readonly SelectedSheetStore _selectedSheetStore;
        private readonly ModalNavigationStore _modalNavigationStore;         
        
        public ModuleListingViewModel(ModuleStore modulStore, SelectedModuleStore selectedModuleStore, SelectedSheetStore selectedSheetStore, ModalNavigationStore modalNavigationStore)
        {
            _modulesStore = modulStore;
            _selectedModuleStore = selectedModuleStore;
            _selectedSheetStore = selectedSheetStore;
            _modalNavigationStore = modalNavigationStore;
            _moduleListingItemViewModel = new ObservableCollection<ModuleListingItemViewModel>();          

            AddModuleCommand = new OpenAddModuleCommand(_modulesStore, _modalNavigationStore);
            DeleteModuleCommand = new DeleteModuleCommand(this, _selectedModuleStore, _modulesStore);
            ModuleItemInsertetCommand = new ModuleItemInsertetCommand(this, _modulesStore);
            OpenEditModuleCommand = new OpenEditModuleCommand(this, modulStore, modalNavigationStore);

            _modulesStore.ModulesLoaded += ModulesStoreModuleLoaded;
            _modulesStore.ModuleAdded += ModulesStoreModuleAdded;
            _modulesStore.ModuleUpdated += ModulesStoreModuleUpdated;
            _modulesStore.ModuleDeleted += ModulesStoreModuleDeleted;
            _modulesStore.ModulesReorded += ModuleStoreReorderModule;

            _selectedModuleStore.SelectedModuleChanged += ModuleStoreModuleChanged;
            _moduleListingItemViewModel.CollectionChanged += ModuleListingItemViewModelCollectionChanged;

        }
               

        #region Properties                

        public ModuleListingItemViewModel SelectedModuleListingItemViewModel
        {
            get
            {
                return _moduleListingItemViewModel.
                    FirstOrDefault(m => m.Module?.Id == _selectedModuleStore.SelectedModule?.Id);
            }
            set
            {          
                _selectedModuleStore.SelectedModule = value?.Module;
            }
        }

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private ModuleListingItemViewModel _insertetModuleItemViewModel;
        public ModuleListingItemViewModel InsertetModuleItemViewModel
        {
            get
            {
                return _insertetModuleItemViewModel;
            }
            set
            {
                _insertetModuleItemViewModel = value;
                OnPropertyChanged(nameof(InsertetModuleItemViewModel));
            }
        }

        private ModuleListingItemViewModel _targetetModuleItemViewModel;
        public ModuleListingItemViewModel TargetetModuleItemViewModel
        {
            get
            {
                return _targetetModuleItemViewModel;
            }
            set
            {
                _targetetModuleItemViewModel = value;
                OnPropertyChanged(nameof(TargetetModuleItemViewModel));
            }
        }

        private ModuleListingItemViewModel _editModuleItemViewModel;
        public ModuleListingItemViewModel EditModuleItemViewModel
        {
            get
            {
                return _editModuleItemViewModel;
            }
            set
            {
                _editModuleItemViewModel = value;
                OnPropertyChanged(nameof(EditModuleItemViewModel));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private readonly ObservableCollection<ModuleListingItemViewModel> _moduleListingItemViewModel;
        public IEnumerable<ModuleListingItemViewModel> ModuleListingItemViewModel =>
            _moduleListingItemViewModel;     

        #endregion

        #region Commands
        public ICommand AddModuleCommand { get; set; }
        public ICommand DeleteModuleCommand { get; set; }
        public ICommand ModuleItemInsertetCommand { get; set; }
        public ICommand OpenEditModuleCommand { get; set; }

        #endregion

        #region Actions

        public override void Dispose()
        {

            _modulesStore.ModulesLoaded -= ModulesStoreModuleLoaded;
            _modulesStore.ModuleAdded -= ModulesStoreModuleAdded;
            _modulesStore.ModuleDeleted -= ModulesStoreModuleDeleted;
            _modulesStore.ModulesReorded -= ModuleStoreReorderModule;

            _selectedModuleStore.SelectedModuleChanged -= ModuleStoreModuleChanged;


            base.Dispose();
        }

        public void ModuleStoreReorderModule(Module insertetModule, Module targetetModule)
        {
            if (insertetModule == targetetModule)
            {
                return;
            }

            var insertetViewModel = _moduleListingItemViewModel.SingleOrDefault(m => m.Module.Id == insertetModule.Id);
            var targetetViewModel = _moduleListingItemViewModel.SingleOrDefault(m => m.Module.Id == targetetModule.Id);

            if(insertetViewModel != null && targetetViewModel != null)
            {
                var oldIndex = _moduleListingItemViewModel.IndexOf(insertetViewModel);
                var nextIndex = _moduleListingItemViewModel.IndexOf(targetetViewModel);

                if (oldIndex != -1 && nextIndex != -1)
                {
                    _moduleListingItemViewModel.Move(oldIndex, nextIndex);
                }
            }            
        }

        private void ModuleStoreModuleChanged()
        {
            OnPropertyChanged(nameof(SelectedModuleListingItemViewModel));
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

        private void ModulesStoreModuleAdded(Module module)
        {
            AddModule(module);
        }

        private void ModuleListingItemViewModelCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedModuleListingItemViewModel));
        }

        private void ModulesStoreModuleUpdated(Module module)
        {
            var moduleViewModel = _moduleListingItemViewModel.FirstOrDefault(m => m.Module.Id == module.Id);

            if (moduleViewModel != null)
            {
                moduleViewModel.Update(module);
            }
        }

        private void AddModule(Module module)
        {
            _moduleListingItemViewModel.Add(new ModuleListingItemViewModel(module, _modulesStore, _selectedSheetStore));            
        }        

        #endregion

    }
}
