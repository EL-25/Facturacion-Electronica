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
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal IVA { get; set; }

        [Required]
        public int TipoItem { get; set; } // Ej: 1 = Bien, 2 = Servicio

        [Required]
        public int UnidadMedida { get; set; } // Ej: 59 = Unidad, 63 = Servicio

        // Relación con Documento
        [ForeignKey("Documento")]
        public int IdDocumento { get; set; }
        public Documento Documento { get; set; }

        [StringLength(20)]
        public string Codigo { get; set; }

    }
}
