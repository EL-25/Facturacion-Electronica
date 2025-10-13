using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionElectronicaSV.Models
{
    public class Resumen
    {
        [Key]
        public int IdResumen { get; set; }

        [Required]
        [Range(0.00, double.MaxValue)]
        public decimal TotalGravado { get; set; }

        [Required]
        [Range(0.00, double.MaxValue)]
        public decimal TotalExento { get; set; }

        [Required]
        [Range(0.00, double.MaxValue)]
        public decimal TotalNoSujeto { get; set; }

        [Required]
        [Range(0.00, double.MaxValue)]
        public decimal IVA { get; set; }

        [Required]
        [Range(0.00, double.MaxValue)]
        public decimal TotalPagar { get; set; }

        // Relación con Documento
        [ForeignKey("Documento")]
        public int IdDocumento { get; set; }
        public Documento Documento { get; set; }
    }
}