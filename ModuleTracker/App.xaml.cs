using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System.Windows;

namespace ModuleTracker.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ModuleStore _moduleStore;
        private readonly SelectedModuleStore _selectedModuleStore;
        private readonly SelectedSheetStore _selectedSheetStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public App()
        {
            _moduleStore = new ModuleStore();

            _selectedModuleStore = new SelectedModuleStore(_moduleStore);
            _selectedSheetStore = new SelectedSheetStore(_moduleStore);
            _modalNavigationStore = new ModalNavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var moduleViewModel = new ModuleViewModel(_moduleStore, _selectedModuleStore, _selectedSheetStore, _modalNavigationStore);

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_modalNavigationStore, moduleViewModel)                
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

    
}
