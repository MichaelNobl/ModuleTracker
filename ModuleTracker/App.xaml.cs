using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Queries;
using ModuleTracker.EntityFramework;
using ModuleTracker.EntityFramework.Commands;
using ModuleTracker.EntityFramework.Queries;
using ModuleTracker.Wpf.HostBuilders;
using ModuleTracker.Wpf.Stores;
using ModuleTracker.Wpf.ViewModel;
using System;
using System.Windows;

namespace ModuleTracker.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder().
                AddDbContext()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IGetAllModulesQuery, GetAllModulesQuery>();
                    services.AddSingleton<ICreateExerciseCommand, CreateExerciseCommand>();
                    services.AddSingleton<ICreateModuleCommand, CreateModuleCommand>();
                    services.AddSingleton<ICreateSheetCommand, CreateSheetCommand>();
                    services.AddSingleton<IDeleteModuleCommand, DeleteModuleCommand>();
                    services.AddSingleton<IDeleteSheetCommand, DeleteSheetCommand>();
                    services.AddSingleton<IUpdateExerciseCommand, UpdateExerciseCommand>();
                    services.AddSingleton<IUpdateSheetCommand, UpdateSheetCommand>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<ModuleStore>();
                    services.AddSingleton<SelectedModuleStore>();
                    services.AddSingleton<SelectedSheetStore>();

                    services.AddTransient<ModuleViewModel>(CreateModuleViewModel);
                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });

                }).Build();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var moduleDbContextFactory = _host.Services.GetRequiredService<ModulesDbContextFactory>();

            using (var context = moduleDbContextFactory.Create())
            {
                context.Database.Migrate();
            } 

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }


        private ModuleViewModel CreateModuleViewModel(IServiceProvider services)
        {
           return ModuleViewModel.LoadViewModel(
               services.GetRequiredService<ModuleStore>(),
               services.GetRequiredService<SelectedModuleStore>(),
               services.GetRequiredService<SelectedSheetStore>(),
               services.GetRequiredService<ModalNavigationStore>());
        }
    }

    
}
