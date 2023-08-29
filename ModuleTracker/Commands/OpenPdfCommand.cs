using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Diagnostics;

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
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "msedge";
            process.StartInfo.Arguments = Uri.EscapeDataString(_sheetListingItemViewModel.Sheet.PdfFilePath);
            process.Start();
        }
    }
}
