using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class TraccionRepositorio : RepositorioGenerico<Traccion, int>, ITraccionRepositorio
    {
        public TraccionRepositorio(DiversosContext context) : base(context) { }
    }
}
