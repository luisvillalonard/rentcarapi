using System;

namespace Diversos.Core.Models
{
    [Serializable]
    public class ResponseResult : IDisposable
    {
        public bool Ok { get; set; } = true;
        public object Datos { get; set; } = null;
        public string Mensaje { get; set; } = null;
        public PagingResult Paginacion { get; set; } = null;

        public ResponseResult() { }

        public ResponseResult(object datos)
        {
            Datos = datos;
        }
        
        public ResponseResult(string mensaje, bool paso)
        {
            Mensaje = mensaje;
            Ok = paso;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
