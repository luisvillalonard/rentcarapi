using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class VehiculoFotoRepositorio : RepositorioGenerico<VehiculoFoto, long>, IVehiculoFotoRepositorio
    {
        public VehiculoFotoRepositorio(DiversosContext context) : base(context) { }
    }
}
