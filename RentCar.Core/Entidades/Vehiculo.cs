using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    [Index(nameof(Codigo), Name = "IDX_Vehiculo_Codigo")]
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Alquiler = new HashSet<Alquiler>();
            VehiculoAccesorio = new HashSet<VehiculoAccesorio>();
            VehiculoFoto = new HashSet<VehiculoFoto>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        public int PersonaId { get; set; }
        public int TipoId { get; set; }
        public int ModeloId { get; set; }
        public int CombustibleId { get; set; }
        public int TransmisionId { get; set; }
        public int TraccionId { get; set; }
        public int InteriorId { get; set; }
        public int ExteriorId { get; set; }
        public int MotorId { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Precio { get; set; }
        public int Puertas { get; set; }
        public int Pasajeros { get; set; }
        public int CargaId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Uso { get; set; }
        public bool EnKilometros { get; set; }
        public bool EnMillas { get; set; }
        public long? FotoId { get; set; }

        [ForeignKey("CargaId")]
        [InverseProperty("Vehiculo")]
        public virtual Carga Carga { get; set; } = null!;
        [ForeignKey("CombustibleId")]
        [InverseProperty("Vehiculo")]
        public virtual Combustible Combustible { get; set; } = null!;
        [ForeignKey("ExteriorId")]
        [InverseProperty("VehiculoExterior")]
        public virtual Color Exterior { get; set; } = null!;
        [ForeignKey("FotoId")]
        [InverseProperty("Vehiculo")]
        public virtual Foto? Foto { get; set; }
        [ForeignKey("InteriorId")]
        [InverseProperty("VehiculoInterior")]
        public virtual Color Interior { get; set; } = null!;
        [ForeignKey("ModeloId")]
        [InverseProperty("Vehiculo")]
        public virtual Modelo Modelo { get; set; } = null!;
        [ForeignKey("MotorId")]
        [InverseProperty("Vehiculo")]
        public virtual Motor Motor { get; set; } = null!;
        [ForeignKey("PersonaId")]
        [InverseProperty("Vehiculo")]
        public virtual Persona Persona { get; set; } = null!;
        [ForeignKey("TipoId")]
        [InverseProperty("Vehiculo")]
        public virtual VehiculoTipo Tipo { get; set; } = null!;
        [ForeignKey("TraccionId")]
        [InverseProperty("Vehiculo")]
        public virtual Traccion Traccion { get; set; } = null!;
        [ForeignKey("TransmisionId")]
        [InverseProperty("Vehiculo")]
        public virtual Transmision Transmision { get; set; } = null!;
        [InverseProperty("Vehiculo")]
        public virtual ICollection<Alquiler> Alquiler { get; set; }
        [InverseProperty("Vehiculo")]
        public virtual ICollection<VehiculoAccesorio> VehiculoAccesorio { get; set; }
        [InverseProperty("Vehiculo")]
        public virtual ICollection<VehiculoFoto> VehiculoFoto { get; set; }
    }
}
