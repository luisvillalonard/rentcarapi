using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Accesorio
    {
        public Accesorio()
        {
            VehiculoAccesorio = new HashSet<VehiculoAccesorio>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [InverseProperty("Accesorio")]
        public virtual ICollection<VehiculoAccesorio> VehiculoAccesorio { get; set; }
    }
}
