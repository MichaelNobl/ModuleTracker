using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Diagnostics;
using System.IO;

namespace ModuleTracker.Wpf.Commands
{
    class OpenPdfCommand : CommandBase
    {
        private readonly SheetListingItemViewModel _sheetListingItemViewModel;

        public OpenPdfCommand(SheetListingItemViewModel sheetListingItemViewModel)
        {
            _sheetListingItemViewModel = sheetListingItemViewModel;
        }

        public override void Execute(object? parameter)
        {
            _sheetListingItemViewModel.ErrorMessage = string.Empty;

            if (File.Exists(_sheetListingItemViewModel.Sheet.PdfFilePath))
            {
                var startInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = "msedge",
                    Arguments = Uri.EscapeDataString(_sheetListingItemViewModel.Sheet.PdfFilePath)
                };

                var process = new Process()
                {
                    StartInfo = startInfo
                };

                process.Start();
            }
            else
            {
                _sheetListingItemViewModel.ErrorMessage = "File might have been moved or deleted.";
            }   
        }
    }
}
