using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    internal class EditModuleCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly EditModuleViewModel _editModuleViewModel;
        private readonly ModuleStore _moduleStore;

        public EditModuleCommand(EditModuleViewModel editModuleViewModel, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            _editModuleViewModel = editModuleViewModel;
            _moduleStore = moduleStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var viewModel = _editModuleViewModel;          

            viewModel.IsSubmitting = true;
            viewModel.ErrorMessage = string.Empty;

            var oldModule = _editModuleViewModel.Module;

            var newModule = new Module(oldModule.Id, _editModuleViewModel.Name, oldModule.Sheets);

            try
            {
                await _moduleStore.UpdateModule(newModule);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                viewModel.ErrorMessage = "Failed to edit module. Please try again later.";
            }
            finally
            {
                viewModel.IsSubmitting = false;
            }
        }
    }
}
