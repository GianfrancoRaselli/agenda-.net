namespace Business.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<Color> Colores { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<ZonaHoraria> ZonasHorarias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Color>()
                .Property(e => e.codigo_hex)
                .IsUnicode(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Eventos)
                .WithRequired(e => e.color)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombre_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.contrasenia)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombre_apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.foto)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Eventos)
                .WithRequired(e => e.usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ZonaHoraria>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ZonaHoraria>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.zonaHoraria)
                .WillCascadeOnDelete(false);
        }
    }
}
