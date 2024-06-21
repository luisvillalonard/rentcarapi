using Diversos.Infraestructure.Contexto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;

namespace Diversos.Infraestructure.Repositories
{
    public class MarcaRepositorio : RepositorioGenerico<Marca, int>, IMarcaRepositorio
    {
        public MarcaRepositorio(DiversosContext context) : base(context) { }
    }
}
