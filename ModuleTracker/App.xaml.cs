using Microsoft.EntityFrameworkCore;
using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Queries;
using ModuleTracker.EntityFramework;
using ModuleTracker.EntityFramework.Commands;
using ModuleTracker.EntityFramework.Queries;
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
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ModuleStore _moduleStore;
        private readonly SelectedModuleStore _selectedModuleStore;

        private readonly ModuleDbContextFactory _moduleDbContextFactory;
        private readonly IGetAllModulesQuery _getAllModulesQuery;
        private readonly ICreateModuleCommand _createModuleCommand;
        private readonly IUpdateModuleCommand _updateModuleCommand;
        private readonly IDeleteModuleCommand _deleteModuleCommand;
        

        public App()
        {
            var connectionString = "Data Source=Modules.db";

            _moduleDbContextFactory = new ModuleDbContextFactory(
                new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            _getAllModulesQuery = new GetAllModulesQuery(_moduleDbContextFactory);
            _createModuleCommand = new CreateModuleCommand(_moduleDbContextFactory);
            _updateModuleCommand = new UpdateModuleCommand(_moduleDbContextFactory);
            _deleteModuleCommand = new DeleteModuleCommand(_moduleDbContextFactory);

            _modalNavigationStore = new ModalNavigationStore();
            _moduleStore = new ModuleStore(_getAllModulesQuery, _createModuleCommand, _updateModuleCommand, _deleteModuleCommand);
            _selectedModuleStore = new SelectedModuleStore(_moduleStore);


        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (ModuleDbContext context = _moduleDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

                MainWindow = new MainWindow
                {
                    DataContext = new ModuleViewModel(_moduleStore, _modalNavigationStore)
                };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

    
}
