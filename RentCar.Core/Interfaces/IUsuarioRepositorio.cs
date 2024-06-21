using Diversos.Core.Entidades;
using Diversos.Core.Models;
using System.Threading.Tasks;

namespace Diversos.Core.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario, int>
    {
        Task<ResponseResult> PostValidarAsync(Usuario item);
    }
}
