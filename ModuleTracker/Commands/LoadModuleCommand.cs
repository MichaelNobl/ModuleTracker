﻿using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class LoadModuleCommand : AsyncCommandBase
    {
        private readonly ModuleViewModel _moduleViewModel;
        private readonly ModuleStore _moduleStore;

        public LoadModuleCommand(ModuleViewModel moduleViewModel, ModuleStore moduleStore)
        {
            _moduleViewModel = moduleViewModel;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _moduleStore.Load();
            }
            catch(Exception) 
            {
            }
            finally
            {
            }
        }
    }
}