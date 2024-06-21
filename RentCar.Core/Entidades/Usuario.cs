using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }
        [Required]
        [StringLength(50)]
        public string Acceso { get; set; }
        [Required]
        [StringLength(2000)]
        public string PasswordHash { get; set; }
        [Required]
        [MaxLength(1000)]
        public byte[] PasswordSalt { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreadoEn { get; set; }
        public bool Cambio { get; set; }
        public int RolId { get; set; }
        public int? PersonaId { get; set; }
        public bool Activo { get; set; }

        [ForeignKey(nameof(PersonaId))]
        [InverseProperty("Usuario")]
        public virtual Persona Persona { get; set; }
        [ForeignKey(nameof(RolId))]
        [InverseProperty("Usuario")]
        public virtual Rol Rol { get; set; }
    }
}
