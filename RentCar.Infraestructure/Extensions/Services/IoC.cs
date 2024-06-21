using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Repositories;
using Diversos.Infraestructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Diversos.Infraestructure.Extensions.Services
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            /* Genericos */
            services.AddTransient(typeof(IRepositorioGenerico<,>), typeof(RepositorioGenerico<,>));
            
            /* Envio de Correo */
            services.AddTransient(typeof(IMailService), typeof(MailService));

            /* Auxiliares */
            services.AddTransient(typeof(IAccesorioRepositorio), typeof(AccesorioRepositorio));
            services.AddTransient(typeof(ICargaRepositorio), typeof(CargaRepositorio));
            services.AddTransient(typeof(IColorRepositorio), typeof(ColorRepositorio));
            services.AddTransient(typeof(ICombustibleRepositorio), typeof(CombustibleRepositorio));
            services.AddTransient(typeof(IEstadoRepositorio), typeof(EstadoRepositorio));
            services.AddTransient(typeof(IFotoRepositorio), typeof(FotoRepositorio));
            services.AddTransient(typeof(IMarcaRepositorio), typeof(MarcaRepositorio));
            services.AddTransient(typeof(IModeloRepositorio), typeof(ModeloRepositorio));
            services.AddTransient(typeof(IMotorRepositorio), typeof(MotorRepositorio));
            services.AddTransient(typeof(IMunicipioRepositorio), typeof(MunicipioRepositorio));
            services.AddTransient(typeof(IProvinciaRepositorio), typeof(ProvinciaRepositorio));
            services.AddTransient(typeof(IRolRepositorio), typeof(RolRepositorio));
            services.AddTransient(typeof(ITraccionRepositorio), typeof(TraccionRepositorio));
            services.AddTransient(typeof(ITransmisionRepositorio), typeof(TransmisionRepositorio));
            services.AddTransient(typeof(IUsoRepositorio), typeof(UsoRepositorio));
            services.AddTransient(typeof(IVehiculoTipoRepositorio), typeof(VehiculoTipoRepositorio));
            services.AddTransient(typeof(IConfiguracionAlquilerNotaRepositorio), typeof(ConfiguracionAlquilerNotaRepositorio));

            /* Entidades */
            services.AddTransient(typeof(IUsuarioRepositorio), typeof(UsuarioRepositorio));
            services.AddTransient(typeof(IPropietarioRepositorio), typeof(PropietarioRepositorio));
            services.AddTransient(typeof(IVehiculoRepositorio), typeof(VehiculoRepositorio));
            services.AddTransient(typeof(IVehiculoFotoRepositorio), typeof(VehiculoFotoRepositorio));
            services.AddTransient(typeof(IVehiculoAccesorioRepositorio), typeof(VehiculoAccesorioRepositorio));
            services.AddTransient(typeof(IPersonaRepositorio), typeof(PersonaRepositorio));
            services.AddTransient(typeof(IAlquilerRepositorio), typeof(AlquilerRepositorio));
            services.AddTransient(typeof(IMensajeRepositorio), typeof(MensajeRepositorio));

            return services;
        }
    }
}
