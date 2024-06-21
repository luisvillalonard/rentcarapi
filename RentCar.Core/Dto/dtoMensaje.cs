namespace Diversos.Core.Dto
{
    public class dtoMensaje
    {
        public int Id { get; set; }
        public int? MensajeId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Comentario { get; set; }
        public string Fecha { get; set; }
        public bool Contestado { get; set; }
    }
}
