﻿using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class AddModuleCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly AddModuleViewModel _addModuleViewModel;
        private readonly ModuleStore _moduleStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddModuleCommand(AddModuleViewModel addModuleViewModel, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            _addModuleViewModel = addModuleViewModel;
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var viewModel = _addModuleViewModel;

            var module = new Module(Guid.NewGuid(), viewModel.Name);
            try
            {
                await _moduleStore.AddModule(module);

                _modalNavigationStore.Close();
            }
            catch(Exception)
            {

            }            
        }
    }
}