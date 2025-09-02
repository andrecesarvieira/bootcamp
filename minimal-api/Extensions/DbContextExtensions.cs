using Microsoft.EntityFrameworkCore;
using MinimalApi.Infrastructure.Data;

namespace MinimalApi.Extensions
{
    public static class DbContextExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContexto>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("MySql"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MySql"))
                )
            );
        }
    }
}