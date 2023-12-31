﻿using Microsoft.Win32;
using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Commands
{
    public class UpdatePdfFilePathCommand : AsyncCommandBase
    {
        private ExercisesViewModel _exerciseItemViewModel;
        private Sheet _sheet;
        private ModuleStore _moduleStore;

        public UpdatePdfFilePathCommand(ExercisesViewModel exerciseItemViewModel, Sheet sheet, ModuleStore moduleStore)
        {
            _exerciseItemViewModel = exerciseItemViewModel;
            _sheet = sheet;
            _moduleStore = moduleStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _exerciseItemViewModel.ErrorMessage = string.Empty;

            var dlg = new OpenFileDialog
            {
                Filter = "pdf files (*.pdf) |*.pdf;"
            };
            dlg.ShowDialog();

            _exerciseItemViewModel.IsSubmitting = true;

            if (string.IsNullOrEmpty(dlg.FileName))
            {
                _exerciseItemViewModel.IsSubmitting = false;
                return;
            }

            _sheet.SetPdfFilePath(dlg.FileName);
            _exerciseItemViewModel.Update(_sheet);

            try
            {
                await _moduleStore.UpdateSheet(_sheet);

            }
            catch (Exception)
            {
                _exerciseItemViewModel.ErrorMessage = "Failed to add pdf file. Please try again later.";
            }
            finally
            {
                _exerciseItemViewModel.IsSubmitting = false;
            }
        }
    }
}
