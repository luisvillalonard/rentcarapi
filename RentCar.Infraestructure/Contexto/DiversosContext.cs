using Diversos.Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Diversos.Infraestructure.Contexto
{
    public partial class DiversosContext : DbContext
    {
        public DiversosContext()
        {
        }

        public DiversosContext(DbContextOptions<DiversosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accesorio> Accesorio { get; set; }
        public virtual DbSet<Alquiler> Alquiler { get; set; }
        public virtual DbSet<AlquilerNota> AlquilerNota { get; set; }
        public virtual DbSet<Carga> Carga { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Combustible> Combustible { get; set; }
        public virtual DbSet<ConfiguracionAlquilerNota> ConfiguracionAlquilerNota { get; set; }
        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Mensaje> Mensaje { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<Motor> Motor { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Traccion> Traccion { get; set; }
        public virtual DbSet<Transmision> Transmision { get; set; }
        public virtual DbSet<Uso> Uso { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }
        public virtual DbSet<VehiculoAccesorio> VehiculoAccesorio { get; set; }
        public virtual DbSet<VehiculoFoto> VehiculoFoto { get; set; }
        public virtual DbSet<VehiculoTipo> VehiculoTipo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accesorio>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Alquiler>(entity =>
            {
                entity.HasOne(d => d.Comprobante)
                    .WithMany(p => p.Alquiler)
                    .HasForeignKey(d => d.ComprobanteId)
                    .HasConstraintName("FK_Alquiler_Foto_FotoId");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Alquiler)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.Alquiler)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AlquilerNota>(entity =>
            {
                entity.Property(e => e.Nota).IsUnicode(false);

                entity.HasOne(d => d.Alquiler)
                    .WithMany(p => p.AlquilerNota)
                    .HasForeignKey(d => d.AlquilerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Carga>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Combustible>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<ConfiguracionAlquilerNota>(entity =>
            {
                entity.Property(e => e.Nota).IsUnicode(false);
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.Property(e => e.Extension).IsUnicode(false);
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.Property(e => e.Correo).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Modelo)
                    .HasForeignKey(d => d.MarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Motor>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.HasOne(d => d.Provincia)
                    .WithMany(p => p.Municipio)
                    .HasForeignKey(d => d.ProvinciaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.Property(e => e.Codigo).IsUnicode(false);

                entity.Property(e => e.Correo).IsUnicode(false);

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.Documento).IsUnicode(false);

                entity.Property(e => e.Licencia).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Telefono1).IsUnicode(false);

                entity.Property(e => e.Telefono2).IsUnicode(false);

                entity.HasOne(d => d.Municipio)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.MunicipioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arrendatario_Municipio_MunicipioId");
            });

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Traccion>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Transmision>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Uso>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Acceso).IsUnicode(false);

                entity.Property(e => e.Codigo).IsUnicode(false);

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.Property(e => e.Codigo).IsUnicode(false);

                entity.HasOne(d => d.Carga)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.CargaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Combustible)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.CombustibleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Exterior)
                    .WithMany(p => p.VehiculoExterior)
                    .HasForeignKey(d => d.ExteriorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Interior)
                    .WithMany(p => p.VehiculoInterior)
                    .HasForeignKey(d => d.InteriorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Modelo)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.ModeloId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Motor)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.MotorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Tipo)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.TipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Traccion)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.TraccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Transmision)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.TransmisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<VehiculoAccesorio>(entity =>
            {
                entity.HasOne(d => d.Accesorio)
                    .WithMany(p => p.VehiculoAccesorio)
                    .HasForeignKey(d => d.AccesorioId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.VehiculoAccesorio)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<VehiculoFoto>(entity =>
            {
                entity.HasOne(d => d.Foto)
                    .WithMany(p => p.VehiculoFoto)
                    .HasForeignKey(d => d.FotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.VehiculoFoto)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<VehiculoTipo>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
