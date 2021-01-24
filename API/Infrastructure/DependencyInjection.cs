using Expenses.Application.Common.Interfaces;
using Expenses.Infrastructure.Files;
using Expenses.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Expenses.Infrastructure.Persistence;
using MongoDB.Driver;
using Expenses.Infrastructure.Persistence.Configurations;
using Expenses.Application.Common.Interfaces.Repository;
using Expenses.Infrastructure.Persistence.Repository;

namespace Expenses.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            MongoConfiguration.Configure();

            var client = new MongoClient(configuration.GetSection("MongoDbSettings").GetSection("ConnectionString").Value);
            var databaseName = configuration.GetSection("MongoDbSettings").GetSection("DatabaseName").Value;
            var database = client.GetDatabase(databaseName);
            services.AddScoped<IMongoContext>(provider => new MongoContext(database, client, provider.GetRequiredService<ICurrentUserService>()));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            return services;
        }
    }
}