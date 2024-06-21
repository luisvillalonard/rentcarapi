using Diversos.Infraestructure.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Diversos.Infraestructure.Extensions.Services
{
    public static class DbContextService
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DiversosContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("ConnDB"));
            });
            return services;
        }
    }
}
