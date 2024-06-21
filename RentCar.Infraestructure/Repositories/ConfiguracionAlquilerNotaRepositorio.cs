using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class ConfiguracionAlquilerNotaRepositorio : RepositorioGenerico<ConfiguracionAlquilerNota, int>, IConfiguracionAlquilerNotaRepositorio
    {
        public ConfiguracionAlquilerNotaRepositorio(DiversosContext context) : base(context) { }
    }
}
