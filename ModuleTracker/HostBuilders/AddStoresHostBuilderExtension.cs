using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleTracker.Domain.Commands;
using ModuleTracker.Domain.Queries;
using ModuleTracker.EntityFramework.Commands;
using ModuleTracker.EntityFramework.Queries;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Wpf.HostBuilders
{
    public static class AddStoresHostBuilderExtension
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ModalNavigationStore>();
                services.AddSingleton<ModuleStore>();
                services.AddSingleton<SelectedModuleStore>();
                services.AddSingleton<SelectedSheetStore>();
            });

            return hostBuilder;
        }
    }
}
