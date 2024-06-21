namespace Diversos.Core.Dto
{
    public class dtoMunicipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public dtoProvincia Provincia { get; set; }
    }
}
