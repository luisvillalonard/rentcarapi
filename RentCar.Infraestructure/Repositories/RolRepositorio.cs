using Diversos.Infraestructure.Contexto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;

namespace Diversos.Infraestructure.Repositories
{
    public class RolRepositorio : RepositorioGenerico<Rol, int>, IRolRepositorio
    {
        public RolRepositorio(DiversosContext context) : base(context) { }
    }
}
