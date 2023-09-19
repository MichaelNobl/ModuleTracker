using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleTracker.Wpf.Stores;

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
