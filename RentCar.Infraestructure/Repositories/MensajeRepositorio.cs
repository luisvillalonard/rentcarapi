using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class MensajeRepositorio : RepositorioGenerico<Mensaje, int>, IMensajeRepositorio
    {
        public MensajeRepositorio(DiversosContext context) : base(context) { }
    }
}
