using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.Stores
{
    public class SelectedModuleStore
    {
		private ModuleStore _moduleStore;

		private Module? _selectedModule;
		public Module? SelectedModule
        {
			get
			{
				return _selectedModule;
			}
			set
			{
                _selectedModule = value;
				SelectedModuleChanged?.Invoke();

            }
		}

		public event Action SelectedModuleChanged;

		public SelectedModuleStore(ModuleStore moduleStore)
		{
			_moduleStore = moduleStore;

            _moduleStore.ModuleAdded += ModuleStoreModuleAdded;
            _moduleStore.ModuleDeleted += ModuleStoreModuleDeleted;
        }

        private void ModuleStoreModuleDeleted(Guid id)
        {
            if (id == SelectedModule?.Id)
            {
                SelectedModule = null;
            }
        }

        private void ModuleStoreModuleAdded(Module module)
        {
            SelectedModule = module;
        }
    }
}
