using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Mensaje
    {
        [Key]
        public int Id { get; set; }
        public int? MensajeId { get; set; }
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Correo { get; set; }
        [Required]
        [StringLength(500)]
        public string Comentario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        public bool Contestado { get; set; }
    }
}
