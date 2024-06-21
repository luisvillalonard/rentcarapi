using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class MunicipioRepositorio : RepositorioGenerico<Municipio, int>, IMunicipioRepositorio
    {
        public MunicipioRepositorio(DiversosContext context) : base(context) { }
    }
}
