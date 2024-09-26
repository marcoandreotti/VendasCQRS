using CrossCutting.Configurations.AppModel;
using Domain.Intefaces;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CrossCutting.Configurations;

public static class ConfigurationExtensions
{
    public static void AddConfigurationLayer(this IServiceCollection services)
    {
        services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

    }
}
