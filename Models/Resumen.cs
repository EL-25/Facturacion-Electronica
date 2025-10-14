using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionElectronicaSV.Models
{
    public class Resumen
    {
        [Key]
        public int IdResumen { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalGravado { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalExento { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalNoSujeto { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal IVA { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPagar { get; set; }

        // Relación con Documento
        [ForeignKey("Documento")]
        public int IdDocumento { get; set; }
        public Documento Documento { get; set; }
    }
}
