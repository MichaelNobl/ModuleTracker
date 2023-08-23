using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ModuleListingItemViewModel : BaseViewModel
    {
        private ModuleStore _moduleStore;

        public ModuleListingItemViewModel(Module module, ModuleStore moduleStore)
        {
            Module = module;
            _moduleStore = moduleStore;
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

            var module = _moduleStore.Modules.FirstOrDefault(m => m.Id == Module.Id);

            if(module != null)
            {
                foreach (var sheet in module.Sheets)
                {
                    foreach (var exercise in sheet.Exercises)
                    {
                        if (exercise.IsCompleted)
                        {
                            counter++;
                        }
                    }
                }
            }            

            return counter.ToString();
        }

        private string CalculateExercises()
        {
            var counter = 0;

            var module = _moduleStore.Modules.FirstOrDefault(m => m.Id == Module.Id);

            if( module != null)
            {
                foreach (var sheet in module.Sheets)
                {
                    foreach (var exercise in sheet.Exercises)
                    {
                        counter++;
                    }
                }
            }            

            return counter.ToString();
        }

        #endregion


    }
}
