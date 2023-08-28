using Microsoft.Win32;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
