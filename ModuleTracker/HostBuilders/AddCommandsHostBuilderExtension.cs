using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Queries;
using ModuleTracker.EntityFramework.Commands;
using ModuleTracker.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.HostBuilders
{
    public static class AddCommandsHostBuilderExtension
    {
        public static IHostBuilder AddCommands(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IGetAllModulesQuery, GetAllModulesQuery>();
                services.AddSingleton<ICreateExerciseCommand, CreateExerciseCommand>();
                services.AddSingleton<ICreateModuleCommand, CreateModuleCommand>();
                services.AddSingleton<ICreateSheetCommand, CreateSheetCommand>();
                services.AddSingleton<IDeleteModuleCommand, DeleteModuleCommand>();
                services.AddSingleton<IDeleteSheetCommand, DeleteSheetCommand>();
                services.AddSingleton<IUpdateExerciseCommand, UpdateExerciseCommand>();
                services.AddSingleton<IUpdateSheetCommand, UpdateSheetCommand>();
                services.AddSingleton<IUpdateModuleCommand, UpdateModuleCommand>();
            });

            return hostBuilder;
        }
    }
}
