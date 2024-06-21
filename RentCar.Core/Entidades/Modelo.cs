using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Modelo
    {
        public Modelo()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public int MarcaId { get; set; }

        [ForeignKey(nameof(MarcaId))]
        [InverseProperty("Modelo")]
        public virtual Marca Marca { get; set; }
        [InverseProperty("Modelo")]
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
