﻿using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class AddModuleCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly AddModuleViewModel _addModuleViewModel;
        private readonly ModuleStore _moduleStore;

        public AddModuleCommand(AddModuleViewModel addModuleViewModel, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            _addModuleViewModel = addModuleViewModel;
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var viewModel = _addModuleViewModel;

            viewModel.IsSubmitting = true;
            viewModel.ErrorMessage = string.Empty;

            var moduleOrder = _moduleStore.Modules.Count() > 0 ? _moduleStore.Modules.Select( m => m.Order).Max() + 1 : 1;

            var module = new Module(Guid.NewGuid(), viewModel.Name, new List<Sheet>(), moduleOrder);
            
            try
            {
                await _moduleStore.AddModule(module);

                _modalNavigationStore.Close();
            }
            catch(Exception)
            {
                viewModel.ErrorMessage = "Failed to add module. Please try again later.";
            }
            finally
            {
                viewModel.IsSubmitting = false;
            }
        }
    }
}
