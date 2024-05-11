using Microsoft.EntityFrameworkCore;

namespace TarjetaBA.Server.Models
{
    public partial class TarjetaContext : DbContext
    {
        public TarjetaContext(DbContextOptions<TarjetaContext> options) : base(options)
        {

        }
        //Se cre el contexto de Base de dato
        public virtual DbSet<Tarjeta> Tarjetas { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        //Se crea modelBuilder para las tablas con su llave primaria y foranea 
        //Inicialmente fue sin relacion y aprendi hacerlo siempre de forma relacional
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.HasKey(e => e.IdTarjeta)
                    .HasName("PK__TARJETA__A3C02A1033A1C444");

                entity.ToTable("Tarjetas");

                entity.Property(e => e.Cuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Apellido)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__COMPRA__09889210DE0C8C69");

                entity.ToTable("Compras");

                entity.Property(e => e.Cuenta)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Descripcion)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.oTarjeta)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdTarjeta)
                    .HasConstraintName("FK_IDTARJETA");
            });


            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago)
                    .HasName("PK__PAGO__098892104566C8C69");

                entity.ToTable("Pagos");

                entity.Property(e => e.Cuenta)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Descripcion)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Abono).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.oCompra)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK_IDCOMPRA");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
