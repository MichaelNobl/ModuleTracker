﻿using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public BaseViewModel CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(ModalNavigationStore modalNavigationStore, ModuleViewModel moduleViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            ModuleViewModel = moduleViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStoreCurrentViewModelChanged;

        }

        public ModuleViewModel ModuleViewModel { get; }

        public override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStoreCurrentViewModelChanged;

            base.Dispose();
        }

        private void ModalNavigationStoreCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }

        

    }
}
