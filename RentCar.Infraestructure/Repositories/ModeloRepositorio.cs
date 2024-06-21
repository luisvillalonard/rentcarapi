using Diversos.Infraestructure.Contexto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;

namespace Diversos.Infraestructure.Repositories
{
    public class ModeloRepositorio : RepositorioGenerico<Modelo, int>, IModeloRepositorio
    {
        public ModeloRepositorio(DiversosContext context) : base(context) { }
    }
}
