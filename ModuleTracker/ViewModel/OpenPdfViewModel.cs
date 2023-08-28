using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class OpenPdfViewModel : BaseViewModel
    {
        private readonly string _outputPath = @"..\..\..\PdfImages\";

        public OpenPdfViewModel(SheetListingItemViewModel sheetListingItemViewModel, ModalNavigationStore modalNavigationStore, ModuleStore moduleStore) 
        {
			var moduleName = moduleStore.Modules.SingleOrDefault(m => m.Id == sheetListingItemViewModel.Sheet.ModuleId)?.Name;

			var pdfFilePath = sheetListingItemViewModel.Sheet.PdfFilePath;
            var pdfName = pdfFilePath.Substring(pdfFilePath.LastIndexOf("\\") + 1, pdfFilePath.Length - pdfFilePath.LastIndexOf("\\") - 5);

            ImageSource = $"{Directory.GetCurrentDirectory()}..\\{_outputPath}{moduleName}_{sheetListingItemViewModel.Sheet.SheetNumber}_{pdfName}.png";
            CloseCommand = new CloseModalCommand(modalNavigationStore);
			
        }

		#region Properties

		private string	_imageSource;
		public string ImageSource
        {
			get
			{
				return _imageSource;
			}
			set
			{
                _imageSource = value;
				OnPropertyChanged(nameof(ImageSource));
			}
		}

		#endregion
		public ICommand CloseCommand { get; private set; }
    }
}
