using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Alquiler
    {
        public Alquiler()
        {
            AlquilerNota = new HashSet<AlquilerNota>();
        }

        [Key]
        public long Id { get; set; }
        public int PersonaId { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaFin { get; set; }
        public int VehiculoId { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Precio { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Efectivo { get; set; }
        public long? ComprobanteId { get; set; }

        [ForeignKey("ComprobanteId")]
        [InverseProperty("Alquiler")]
        public virtual Foto? Comprobante { get; set; }
        [ForeignKey("PersonaId")]
        [InverseProperty("Alquiler")]
        public virtual Persona Persona { get; set; } = null!;
        [ForeignKey("VehiculoId")]
        [InverseProperty("Alquiler")]
        public virtual Vehiculo Vehiculo { get; set; } = null!;
        [InverseProperty("Alquiler")]
        public virtual ICollection<AlquilerNota> AlquilerNota { get; set; }
    }
}
