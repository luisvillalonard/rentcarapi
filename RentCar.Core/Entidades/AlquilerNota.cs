using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class AlquilerNota
    {
        [Key]
        public int Id { get; set; }
        public long AlquilerId { get; set; }
        [Required]
        [StringLength(500)]
        public string Nota { get; set; }

        [ForeignKey(nameof(AlquilerId))]
        [InverseProperty("AlquilerNota")]
        public virtual Alquiler Alquiler { get; set; }
    }
}
