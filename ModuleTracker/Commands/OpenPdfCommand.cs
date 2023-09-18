using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Diagnostics;
using System.IO;

namespace ModuleTracker.Wpf.Commands
{
    class OpenPdfCommand : CommandBase
    {
        private readonly ExercisesViewModel _exerciseViewModel;

        public OpenPdfCommand(ExercisesViewModel exerciseViewModel)
        {
            _exerciseViewModel = exerciseViewModel;
        }

        public override void Execute(object? parameter)
        {
            _exerciseViewModel.ErrorMessage = string.Empty;

            if (File.Exists(_exerciseViewModel.Sheet.PdfFilePath))
            {
                var startInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = "msedge",
                    Arguments = Uri.EscapeDataString(_exerciseViewModel.Sheet.PdfFilePath)
                };

                var process = new Process()
                {
                    StartInfo = startInfo
                };

                process.Start();
            }
            else
            {
                _exerciseViewModel.ErrorMessage = "File might have been moved or deleted.";
            }   
        }
    }
}
