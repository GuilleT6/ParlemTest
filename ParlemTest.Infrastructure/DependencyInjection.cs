using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ParlemTest.Domain.Configurations;
using ParlemTest.Domain.Interfaces;
using ParlemTest.Domain.Repositories;
using ParlemTest.Infrastructure.Repositories;

namespace ParlemTest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, MongoDbSettings mongoSettings)
        {
            if (string.IsNullOrWhiteSpace(mongoSettings.ConnectionString))
                throw new ArgumentNullException(nameof(mongoSettings.ConnectionString), "MongoDB connection string is null or empty.");

            if (string.IsNullOrWhiteSpace(mongoSettings.DatabaseName))
                throw new ArgumentNullException(nameof(mongoSettings.DatabaseName), "MongoDB database name is null or empty.");

            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoSettings.ConnectionString));

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoSettings.DatabaseName);
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
