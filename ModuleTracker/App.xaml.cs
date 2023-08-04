using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.View;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ModuleTracker.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly ModuleStore _moduleStore;
        private readonly SheetStore _sheetStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public App()
        {
            _moduleStore = new ModuleStore();
            _sheetStore = new SheetStore();

            _selectedModuleStore = new SelectedModuleStore(_moduleStore);
            _modalNavigationStore = new ModalNavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var moduleViewModel = new ModuleViewModel(_moduleStore, _sheetStore, _selectedModuleStore, _modalNavigationStore);

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_modalNavigationStore, moduleViewModel)                
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

    
}
