using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Municipio
    {
        public Municipio()
        {
            Persona = new HashSet<Persona>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public int ProvinciaId { get; set; }

        [ForeignKey(nameof(ProvinciaId))]
        [InverseProperty("Municipio")]
        public virtual Provincia Provincia { get; set; }
        [InverseProperty("Municipio")]
        public virtual ICollection<Persona> Persona { get; set; }
    }
}
