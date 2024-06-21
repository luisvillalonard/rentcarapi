using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class VehiculoFoto
    {
        [Key]
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public long FotoId { get; set; }

        [ForeignKey(nameof(FotoId))]
        [InverseProperty("VehiculoFoto")]
        public virtual Foto Foto { get; set; }
        [ForeignKey(nameof(VehiculoId))]
        [InverseProperty("VehiculoFoto")]
        public virtual Vehiculo Vehiculo { get; set; }
    }
}
