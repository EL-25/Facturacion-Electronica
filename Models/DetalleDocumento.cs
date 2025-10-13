using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionElectronicaSV.Models
{
    public class DetalleDocumento
    {
        [Key]
        public int IdDetalle { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Range(0.00, double.MaxValue)]
        public decimal MontoTotal { get; set; }

        // Relación con Documento
        [ForeignKey("Documento")]
        public int IdDocumento { get; set; }
        public Documento Documento { get; set; }
    }
}