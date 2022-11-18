using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace BD;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddDbContext<AspNetContext>(options =>
        {
            options.UseSqlite("Data Source=folders.db");
        });
    }
}
