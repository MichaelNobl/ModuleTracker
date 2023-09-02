using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace ModuleTracker.Wpf.Commands
{
    public class DeleteModuleCommand : AsyncCommandBase
    {
        private readonly ModuleListingViewModel _moduleListingViewModel;
        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly ModuleStore _moduleStore;

        private readonly string _message = "Do you want to delete module";
        private readonly string _messageCaption = "Delete Module";

        public DeleteModuleCommand(ModuleListingViewModel moduleListingViewModel, SelectedModuleStore selectedModuleStore, ModuleStore moduleStore) 
        {
            _moduleListingViewModel = moduleListingViewModel;
            _selectedModuleStore = selectedModuleStore;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if(_selectedModuleStore.SelectedModule != null)
            {
                var result = MessageBox.Show($"{_message} {_selectedModuleStore.SelectedModule.Name}?", _messageCaption, MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    return;
                }

                _moduleListingViewModel.IsDeleting = true;
                _moduleListingViewModel.ErrorMessage = string.Empty;

                var module = _moduleStore.Modules.SingleOrDefault(m => m.Id == _selectedModuleStore.SelectedModule.Id);

                if (module == null)
                {
                    return;
                }

                var sheetIds = module.Sheets.Select(s => s.Id).ToList();

                foreach (var sheetId in sheetIds)
                {
                    try
                    {
                        await _moduleStore.DeleteSheet(sheetId);
                    }
                    catch (Exception)
                    {
                        _moduleListingViewModel.ErrorMessage = "Failed to delete sheets. Please try again later.";
                        _moduleListingViewModel.IsDeleting = false;
                        return;
                    }
                }

                try
                {
                    if (_selectedModuleStore.SelectedModule != null)
                    {
                        await _moduleStore.DeleteModule(_selectedModuleStore.SelectedModule.Id);
                    }
                }
                catch (Exception)
                {
                    _moduleListingViewModel.ErrorMessage = "Failed to delete module. Please try again later.";
                }
                finally
                {
                    _moduleListingViewModel.IsDeleting = false;
                }
            }            
        }
    }
}
