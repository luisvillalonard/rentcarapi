namespace Diversos.Core.Dto
{
    public class dtoPersona
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public bool EsCedula { get; set; }
        public string Licencia { get; set; }
        public string Direccion { get; set; }
        public dtoMunicipio Municipio { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Correo { get; set; }
        public dtoUsuario Usuario { get; set; }
        public dtoFoto Foto { get; set; }
    }
}
