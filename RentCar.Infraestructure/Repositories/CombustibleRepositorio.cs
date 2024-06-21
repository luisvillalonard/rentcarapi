using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class CombustibleRepositorio : RepositorioGenerico<Combustible, int>, ICombustibleRepositorio
    {
        public CombustibleRepositorio(DiversosContext context) : base(context) { }
    }
}
