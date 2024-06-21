using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class FotoRepositorio : RepositorioGenerico<Foto, long>, IFotoRepositorio
    {
        public FotoRepositorio(DiversosContext context) : base(context) { }
    }
}
