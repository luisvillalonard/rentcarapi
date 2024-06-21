namespace Diversos.Core.Dto
{
    public class dtoUsuario
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Acceso { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string CreadoEn { get; set; }
        public bool Cambio { get; set; }
        public dtoRol Rol { get; set; }
        public dtoPersona Persona { get; set; }
        public bool Activo { get; set; }
    }
}
