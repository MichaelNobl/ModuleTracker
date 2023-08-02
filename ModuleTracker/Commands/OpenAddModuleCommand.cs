﻿using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class OpenAddModuleCommand : CommandBase
    {
        private readonly ModuleStore _moduleStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddModuleCommand(ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            var addYouTubeViewerModel = new AddModuleViewModel(_moduleStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addYouTubeViewerModel;
        }    
    }
}
