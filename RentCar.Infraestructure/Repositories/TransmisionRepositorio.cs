using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class TransmisionRepositorio : RepositorioGenerico<Transmision, int>, ITransmisionRepositorio
    {
        public TransmisionRepositorio(DiversosContext context) : base(context) { }
    }
}
