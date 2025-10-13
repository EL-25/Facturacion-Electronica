using Microsoft.EntityFrameworkCore;
using FacturacionElectronicaSV.Models;

namespace FacturacionElectronicaSV.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Emisor> Emisores { get; set; }
        public DbSet<Receptor> Receptores { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<DetalleDocumento> DetallesDocumento { get; set; }
        public DbSet<Resumen> Resumenes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
