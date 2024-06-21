namespace Diversos.Core.Dto
{
    public class dtoUsuarioApp
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Acceso { get; set; }
        public dtoRol Rol { get; set; }
        public dtoPersona Persona { get; set; }
        public string Token { get; set; }
    }
}
