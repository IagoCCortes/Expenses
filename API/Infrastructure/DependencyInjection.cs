using Expenses.Application.Common.Interfaces;
using Expenses.Infrastructure.Files;
using Expenses.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Expenses.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Expenses.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDbSettings").GetSection("ConnectionString").Value);
            var databaseName = configuration.GetSection("MongoDbSettings").GetSection("DatabaseName").Value;
            var database = client.GetDatabase(databaseName);
            services.AddSingleton<IMongoContext>(new MongoContext(database, client));
            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            return services;
        }
    }
}