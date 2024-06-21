using System.Collections.Generic;

namespace Diversos.Core.Dto
{
    public class dtoAlquiler
    {
        public long Id { get; set; }
        public dtoPersona Persona { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public dtoVehiculo Vehiculo { get; set; }
        public decimal Precio { get; set; }
        public decimal Efectivo { get; set; }
        public dtoFoto Comprobante { get; set; }
        public List<dtoAlquilerNota> Notas { get; set; }

        public dtoAlquiler()
        {
            Notas = new();
        }
    }
}
