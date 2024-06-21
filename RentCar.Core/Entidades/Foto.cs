using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Foto
    {
        public Foto()
        {
            Alquiler = new HashSet<Alquiler>();
            Persona = new HashSet<Persona>();
            Vehiculo = new HashSet<Vehiculo>();
            VehiculoFoto = new HashSet<VehiculoFoto>();
        }

        [Key]
        public long Id { get; set; }
        public byte[] Imagen { get; set; } = null!;
        [StringLength(10)]
        public string Extension { get; set; } = null!;

        [InverseProperty("Comprobante")]
        public virtual ICollection<Alquiler> Alquiler { get; set; }
        [InverseProperty("Foto")]
        public virtual ICollection<Persona> Persona { get; set; }
        [InverseProperty("Foto")]
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
        [InverseProperty("Foto")]
        public virtual ICollection<VehiculoFoto> VehiculoFoto { get; set; }
    }
}
