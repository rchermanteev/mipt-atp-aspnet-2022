using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFolderService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IFolderService, FolderService>();
    }
}
