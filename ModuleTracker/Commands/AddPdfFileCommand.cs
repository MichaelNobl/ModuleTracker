﻿using Microsoft.Win32;
using ModuleTracker.Wpf.ViewModel;

namespace ModuleTracker.Wpf.Commands
{
    public class AddPdfFileCommand : CommandBase
    {
        private readonly AddSheetViewModel _addSheetViewModel;

        public AddPdfFileCommand(AddSheetViewModel addSheetViewModel) 
        {
            _addSheetViewModel = addSheetViewModel;
        }        

        public override void Execute(object? parameter)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "pdf files (*.pdf) |*.pdf;";
            dlg.ShowDialog();

            _addSheetViewModel.PdfFilePath = dlg.FileName;
        }
    }
}
