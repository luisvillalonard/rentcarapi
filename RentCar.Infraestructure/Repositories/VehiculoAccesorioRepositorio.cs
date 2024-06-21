using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class VehiculoAccesorioRepositorio : RepositorioGenerico<VehiculoAccesorio, int>, IVehiculoAccesorioRepositorio
    {
        public VehiculoAccesorioRepositorio(DiversosContext context) : base(context) { }
    }
}
