using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class VehiculoTipoRepositorio : RepositorioGenerico<VehiculoTipo, int>, IVehiculoTipoRepositorio
    {
        public VehiculoTipoRepositorio(DiversosContext context) : base(context) { }
    }
}
