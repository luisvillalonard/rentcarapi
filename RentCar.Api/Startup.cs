using Diversos.Infraestructure.Extensions.ApplicationBuilder;
using Diversos.Infraestructure.Extensions.Services;
using Diversos.Infraestructure.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Diversos.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* Mapeo de entidades y los Objetos de Transferencia de Datos (DTO) */
            services.AddAutoMapper(typeof(Program).Assembly, typeof(AutoMapperProfile).Assembly);

            /* Controladores y extensiones */
            ControllersServices.AddControllersExtend(services, Configuration);

            /* Contexto de Base de Datos*/
            DbContextService.AddDbContext(services, Configuration);

            /* Contenedor de Inversión de Control (IoC)  */
            IoC.AddDependency(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DefaultConfig.InitConfigurationApi(app, env);
        }
    }
}
