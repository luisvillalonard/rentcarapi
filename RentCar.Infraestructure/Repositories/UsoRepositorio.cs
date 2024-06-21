using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class UsoRepositorio : RepositorioGenerico<Uso, int>, IUsoRepositorio
    {
        public UsoRepositorio(DiversosContext context) : base(context) { }
    }
}
