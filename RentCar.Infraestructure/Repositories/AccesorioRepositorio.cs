using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class AccesorioRepositorio : RepositorioGenerico<Accesorio, int>, IAccesorioRepositorio
    {
        public AccesorioRepositorio(DiversosContext context) : base(context) { }
    }
}
