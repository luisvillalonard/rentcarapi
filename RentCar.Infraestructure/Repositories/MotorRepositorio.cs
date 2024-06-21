using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class MotorRepositorio : RepositorioGenerico<Motor, int>, IMotorRepositorio
    {
        public MotorRepositorio(DiversosContext context) : base(context) { }
    }
}
