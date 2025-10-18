using Microsoft.EntityFrameworkCore;

namespace Documento.Infraestructura.Context
{
    public class DocumentoContext : DbContext
    {

        public DocumentoContext(DbContextOptions<DocumentoContext> options) : base(options)
        {
        }

        public DbSet<Dominio.Entidades.Documento> Documentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Dominio.Entidades.Documento>(entidad =>
            {

                entidad.HasKey(x => x.Id);

                entidad.OwnsOne(titulo => titulo.Titulo, p =>
                {
                    p.Property(pp => pp.Valor).HasColumnName("Titulo").HasMaxLength(200).IsRequired();
                });

                entidad.OwnsOne(autor => autor.Autor, p =>
                {
                    p.Property(pp => pp.Valor).HasColumnName("Autor").HasMaxLength(100).IsRequired();
                });

                entidad.OwnsOne(tipo => tipo.Tipo, p =>
                {
                    p.Property(pp => pp.Valor).HasColumnName("Tipo").HasMaxLength(20).IsRequired();
                });

                entidad.OwnsOne(estado => estado.Estado, p =>
                {
                    p.Property(pp => pp.Valor).HasColumnName("Estado").HasMaxLength(20).IsRequired();
                });

                entidad.Property(en => en.FechaRegistro).HasConversion<DateTime>();
            });
        }
    }
}
