using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class ConfiguracionAlquilerNota
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Nota { get; set; }
    }
}
