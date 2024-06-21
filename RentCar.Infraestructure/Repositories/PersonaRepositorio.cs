using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class PersonaRepositorio : RepositorioGenerico<Persona, int>, IPersonaRepositorio
    {
        public PersonaRepositorio(DiversosContext context) : base(context) { }
    }
}
