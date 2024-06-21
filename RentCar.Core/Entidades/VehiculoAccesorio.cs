using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class VehiculoAccesorio
    {
        [Key]
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public int AccesorioId { get; set; }

        [ForeignKey(nameof(AccesorioId))]
        [InverseProperty("VehiculoAccesorio")]
        public virtual Accesorio Accesorio { get; set; }
        [ForeignKey(nameof(VehiculoId))]
        [InverseProperty("VehiculoAccesorio")]
        public virtual Vehiculo Vehiculo { get; set; }
    }
}
