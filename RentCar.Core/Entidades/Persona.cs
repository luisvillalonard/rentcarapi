using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diversos.Core.Entidades
{
    public partial class Persona
    {
        public Persona()
        {
            Alquiler = new HashSet<Alquiler>();
            Usuario = new HashSet<Usuario>();
            Vehiculo = new HashSet<Vehiculo>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        [StringLength(250)]
        public string Nombre { get; set; } = null!;
        [StringLength(20)]
        public string Documento { get; set; } = null!;
        public bool EsCedula { get; set; }
        [StringLength(50)]
        public string Licencia { get; set; }
        public int MunicipioId { get; set; }
        [StringLength(250)]
        public string Direccion { get; set; } = null!;
        [StringLength(50)]
        public string Telefono1 { get; set; } = null!;
        [StringLength(50)]
        public string Telefono2 { get; set; }
        [StringLength(150)]
        public string Correo { get; set; } = null!;
        public long? FotoId { get; set; }

        [ForeignKey("FotoId")]
        [InverseProperty("Persona")]
        public virtual Foto Foto { get; set; }
        [ForeignKey("MunicipioId")]
        [InverseProperty("Persona")]
        public virtual Municipio Municipio { get; set; } = null!;
        [InverseProperty("Persona")]
        public virtual ICollection<Alquiler> Alquiler { get; set; }
        [InverseProperty("Persona")]
        public virtual ICollection<Usuario> Usuario { get; set; }
        [InverseProperty("Persona")]
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
