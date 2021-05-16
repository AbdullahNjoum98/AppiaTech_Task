using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Task_API.ServicesConfig
{
    public static class ServicesConfigurator
    {
        public static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("TaskDB");


            services.AddDbContext<ProjectDbContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("TaskAPI").UseNetTopologySuite()));


            return services;
        }
    }
}
