using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Color
    {
        public Color()
        {
            VehiculoExterior = new HashSet<Vehiculo>();
            VehiculoInterior = new HashSet<Vehiculo>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [InverseProperty(nameof(Vehiculo.Exterior))]
        public virtual ICollection<Vehiculo> VehiculoExterior { get; set; }
        [InverseProperty(nameof(Vehiculo.Interior))]
        public virtual ICollection<Vehiculo> VehiculoInterior { get; set; }
    }
}
