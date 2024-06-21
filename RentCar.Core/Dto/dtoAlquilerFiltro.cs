namespace Diversos.Core.Dto
{
    public class dtoAlquilerFiltro
    {
        public int? MarcaId { get; set; }
        public int? ModeloId { get; set; }
        public int? CombustibleId { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}
