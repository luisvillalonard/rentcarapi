using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class CargaRepositorio : RepositorioGenerico<Carga, int>, ICargaRepositorio
    {
        public CargaRepositorio(DiversosContext context) : base(context) { }
    }
}
