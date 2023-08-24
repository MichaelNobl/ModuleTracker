using Microsoft.EntityFrameworkCore;
using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Queries;
using ModuleTracker.EntityFramework;
using ModuleTracker.EntityFramework.Commands;
using ModuleTracker.EntityFramework.Queries;
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

        private readonly ModulesDbContextFactory _modulesDbContextFactory;
        private readonly IGetAllModulesQuery _getAllModulesQuery;
        private readonly ICreateExerciseCommand _createExerciseCommand;
        private readonly ICreateModuleCommand _createModuleCommand;
        private readonly ICreateSheetCommand _createSheetCommand;
        private readonly IDeleteModuleCommand _deleteModuleCommand;
        private readonly IDeleteSheetCommand _deleteSheetCommand;
        private readonly IUpdateExerciseCommand _updateExerciseCommand;
        private readonly IUpdateSheetCommand _updateSheetCommand;

        public App()
        {
            string connectionString = "Data Source=Modules.db";

            _modulesDbContextFactory = new ModulesDbContextFactory(
                new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            _getAllModulesQuery = new GetAllModulesQuery(_modulesDbContextFactory);
            _createExerciseCommand = new CreateExerciseCommand(_modulesDbContextFactory);
            _createModuleCommand = new CreateModuleCommand(_modulesDbContextFactory);
            _createSheetCommand = new CreateSheetCommand(_modulesDbContextFactory);
            _deleteModuleCommand = new DeleteModuleCommand(_modulesDbContextFactory);
            _deleteSheetCommand = new DeleteSheetCommand(_modulesDbContextFactory);
            _updateExerciseCommand = new UpdateExerciseCommand(_modulesDbContextFactory);
            _updateSheetCommand = new UpdateSheetCommand(_modulesDbContextFactory);

            _moduleStore = new ModuleStore(
                _getAllModulesQuery,
                _createExerciseCommand,
                _createModuleCommand,
                _createSheetCommand,
                _deleteModuleCommand,
                _deleteSheetCommand,
                _updateExerciseCommand,
                _updateSheetCommand);

            _selectedModuleStore = new SelectedModuleStore(_moduleStore);
            _selectedSheetStore = new SelectedSheetStore(_moduleStore);
            _modalNavigationStore = new ModalNavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (ModulesDbContext context = _modulesDbContextFactory.Create())
            {
                context.Database.Migrate();
            } 

            var moduleViewModel = ModuleViewModel.LoadViewModel(_moduleStore, _selectedModuleStore, _selectedSheetStore, _modalNavigationStore);

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_modalNavigationStore, moduleViewModel)                
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

    
}
