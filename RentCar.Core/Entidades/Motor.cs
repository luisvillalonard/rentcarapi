﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Motor
    {
        public Motor()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [InverseProperty("Motor")]
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
