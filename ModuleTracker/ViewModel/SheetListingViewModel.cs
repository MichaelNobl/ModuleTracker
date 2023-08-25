﻿using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class SheetListingViewModel : BaseViewModel
    {
        private readonly ObservableCollection<SheetListingItemViewModel> _sheetListingItemViewModel;

        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly SelectedSheetStore _selectedSheetStore;
        private readonly ModuleStore _moduleStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private Module _selectedModule => _selectedModuleStore.SelectedModule;
        private Sheet _selectedSheet => _selectedSheetStore.SelectedSheet;

        public SheetListingViewModel(ModuleStore moduleStore, SelectedModuleStore selectedModuleStore, SelectedSheetStore selectedSheetStore, ModalNavigationStore modalNavigationStore)
        {
            _selectedModuleStore = selectedModuleStore;
            _selectedSheetStore = selectedSheetStore;
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;

            _sheetListingItemViewModel = new ObservableCollection<SheetListingItemViewModel>();

            _selectedModuleStore.SelectedModuleChanged += SelectedModuleStoreSelectedModuleChanged;
            _selectedSheetStore.SelectedSheetChanged += SelectedSheetStoreSelectedSheetChanged;
            _sheetListingItemViewModel.CollectionChanged += SheetStoreListingItemViewModelCollectionChanged;

            _moduleStore.SheetsLoaded += ModuleStoreSheetLoaded;
            _moduleStore.SheetAdded += ModuleStoreSheetAdded;
            _moduleStore.SheetUpdated+= ModuleStoreSheetUpdated;
            _moduleStore.SheetDeleted += ModuleStoreSheetDeleted;

            AddSheetCommand = new OpenAddSheetCommand(modalNavigationStore, moduleStore, _selectedModuleStore);
            DeleteSheetCommand = new DeleteSheetCommand(this, selectedSheetStore, moduleStore);
        }

        #region Properties

        private SheetListingItemViewModel _selectedSheetListingItemViewModel;
        public SheetListingItemViewModel SelectedSheetListingItemViewModel
        {
            get
            {
                return _selectedSheetListingItemViewModel;
            }
            set
            {
                _selectedSheetListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedSheetListingItemViewModel));

                _selectedSheetStore.SelectedSheet = _selectedSheetListingItemViewModel?.Sheet;
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

        public bool HasSelectedModule => _selectedModule != null;
        public bool HasSelectedSheet => _selectedSheet != null;
        public IEnumerable<SheetListingItemViewModel> SheetListingItemViewModel =>
            _sheetListingItemViewModel;

        #endregion

        #region Commands
        public ICommand AddSheetCommand { get; }
        public ICommand DeleteSheetCommand { get; }
        #endregion

        #region Actions
        public override void Dispose()
        {
            _selectedModuleStore.SelectedModuleChanged -= SelectedModuleStoreSelectedModuleChanged;
            _selectedSheetStore.SelectedSheetChanged -= SelectedSheetStoreSelectedSheetChanged;

            _moduleStore.SheetsLoaded -= ModuleStoreSheetLoaded;
            _moduleStore.SheetAdded -= ModuleStoreSheetAdded;
            _moduleStore.SheetUpdated -= ModuleStoreSheetUpdated;
            _moduleStore.SheetDeleted -= ModuleStoreSheetDeleted;

            _sheetListingItemViewModel.CollectionChanged -= SheetStoreListingItemViewModelCollectionChanged;

            base.Dispose();
        }

        private void SheetStoreListingItemViewModelCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedSheetListingItemViewModel));
        }

        private void SelectedSheetStoreSelectedSheetChanged()
        {
            OnPropertyChanged(nameof(SelectedSheetListingItemViewModel));
        }       

        private void ModuleStoreSheetAdded(Sheet sheet)
        {
            _sheetListingItemViewModel.Add(new SheetListingItemViewModel(sheet, _modalNavigationStore, _moduleStore));
            OnPropertyChanged(nameof(SheetListingItemViewModel));
        }

        private void ModuleStoreSheetDeleted(Guid id)
        {
            var itemViewModel = _sheetListingItemViewModel.FirstOrDefault(y => y.Sheet.Id == id);

            if (itemViewModel != null)
            {
                _sheetListingItemViewModel.Remove(itemViewModel);
            }
        }

        private void ModuleStoreSheetLoaded()
        {
            _sheetListingItemViewModel.Clear();

            var module = _moduleStore.Modules.FirstOrDefault(m => m.Id == _selectedModule.Id);

            if(module != null)
            {
                foreach (var sheet in module.Sheets)
                {
                    AddSheet(sheet);
                }
            }
        }

        private void ModuleStoreSheetUpdated(Sheet sheet)
        {
            var sheetViewModel = _sheetListingItemViewModel.FirstOrDefault(s => s.Sheet.Id == sheet.Id);

            sheetViewModel?.Update(sheet);
        }


        private void SelectedModuleStoreSelectedModuleChanged()
        {
            AddSheetItemViewModels();
            OnPropertyChanged(nameof(HasSelectedModule));
            OnPropertyChanged(nameof(HasSelectedSheet));
            OnPropertyChanged(nameof(SheetListingItemViewModel));
            OnPropertyChanged(nameof(SelectedSheetListingItemViewModel));
        }

        private void AddSheetItemViewModels()
        {
            _sheetListingItemViewModel.Clear();

            if (_selectedModule != null)
            {
                foreach (var sheet in _selectedModule.Sheets)
                {
                    _sheetListingItemViewModel.Add(new SheetListingItemViewModel(sheet, _modalNavigationStore, _moduleStore));
                }
            }
        }

        private void AddSheet(Sheet sheet)
        {
            var itemViewModel = new SheetListingItemViewModel(sheet, _modalNavigationStore, _moduleStore);
            _sheetListingItemViewModel.Add(itemViewModel);
        }

        #endregion
    }
}
