using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleListingItemViewModel : BaseViewModel
    {       
        public ModuleListingItemViewModel(Module module)
        {
            Module = module;
        }

        #region Properties
        public Module Module { get; private set; }

        public string Name => Module.Name;

        public string NumOfDoneExercises => CalculateDoneExercises();

        public string NumOfExercises => CalculateExercises();

        #endregion

        #region Methods
        private string CalculateDoneExercises()
        {
            var counter = 0;

            foreach (var sheet in Module.Sheets)
            {
                foreach (var exercise in sheet.Exercises)
                {
                    if (exercise.IsCompleted)
                    {
                        counter++;
                    }
                }
            }

            return counter.ToString();
        }

        private string CalculateExercises()
        {
            var counter = 0;

            foreach (var sheet in Module.Sheets)
            {
                foreach (var exercise in sheet.Exercises)
                {
                    counter++;
                }
            }

            return counter.ToString();
        }

        #endregion


    }
}
