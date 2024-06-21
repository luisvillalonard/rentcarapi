using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class EstadoRepositorio : RepositorioGenerico<Estado, int>, IEstadoRepositorio
    {
        public EstadoRepositorio(DiversosContext context) : base(context) { }
    }
}
