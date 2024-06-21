using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class ProvinciaRepositorio : RepositorioGenerico<Provincia, int>, IProvinciaRepositorio
    {
        public ProvinciaRepositorio(DiversosContext context) : base(context) { }
    }
}
