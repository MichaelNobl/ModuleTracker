using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class SheetListingItemViewModel : BaseViewModel
    {
        public Sheet Sheet { get; private set; }

        public string Name => $"Sheet {Sheet.SheetNumber}";

        public string NumOfExercises => Sheet.NumOfExercises.ToString();

        public SheetListingItemViewModel(Sheet sheet)
        {
            Sheet = sheet;
        }
    }
}
