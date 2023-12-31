﻿using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class LoadModulesCommand : AsyncCommandBase
    {
        private readonly ModuleViewModel _moduleViewModel;
        private readonly ModuleStore _moduleStore;

        public LoadModulesCommand(ModuleViewModel moduleViewModel, ModuleStore moduleStore) 
        {
            _moduleViewModel = moduleViewModel;
            _moduleStore = moduleStore;        
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _moduleViewModel.ErrorMessage = string.Empty;
            _moduleViewModel.IsLoading = true;

            try
            {
                await _moduleStore.LoadModules();
            }
            catch (Exception)
            {
                _moduleViewModel.ErrorMessage = "Failed to load modules. Please restart the application.";
            }
            finally
            {
                _moduleViewModel.IsLoading = false;
            }
        }
    }
}
