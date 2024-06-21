using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Infraestructure.Contexto;

namespace Diversos.Infraestructure.Repositories
{
    public class ColorRepositorio : RepositorioGenerico<Color, int>, IColorRepositorio
    {
        public ColorRepositorio(DiversosContext context) : base(context) { }
    }
}
