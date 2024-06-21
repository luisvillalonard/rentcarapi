using System;

namespace Diversos.Core.Dto
{
    public class dtoVehiculo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public dtoPersona Persona { get; set; }
        public dtoVehiculoTipo Tipo { get; set; }
        public dtoModelo Modelo { get; set; }
        public dtoCombustible Combustible { get; set; }
        public dtoTransmision Transmision { get; set; }
        public dtoTraccion Traccion { get; set; }
        public dtoColor Interior { get; set; }
        public dtoColor Exterior { get; set; }
        public dtoMotor Motor { get; set; }
        public decimal Precio { get; set; }
        public int Puertas { get; set; }
        public int Pasajeros { get; set; }
        public dtoCarga Carga { get; set; }
        public decimal Uso { get; set; }
        public bool EnKilometros { get; set; }
        public bool EnMillas { get; set; }
        public dtoFoto Foto { get; set; }
        public dtoAccesorio[] Accesorios { get; set; }
        public dtoFoto[] Fotos { get; set; }

        public dtoVehiculo()
        {
            Accesorios = Array.Empty<dtoAccesorio>();
            Fotos = Array.Empty<dtoFoto>();
        }
    }
}
