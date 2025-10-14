using System.ComponentModel.DataAnnotations;

namespace FacturacionElectronicaSV.Models
{
    public class Receptor
    {
        [Key]
        public int IdReceptor { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(2)]
        public string TipoDocumento { get; set; } // Ej: "36" = DUI, "13" = NIT, "03" = Pasaporte

        [Required]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }

        [StringLength(10)]
        public string CodActividad { get; set; } // ← requerido por Hacienda

        [StringLength(100)]
        public string DescActividad { get; set; } // ← requerido por Hacienda

        [StringLength(100)]
        public string Correo { get; set; } // ← requerido por Hacienda

        [StringLength(200)]
        public string Complemento { get; set; } // ← parte de Dirección

        [StringLength(50)]
        public string Municipio { get; set; }

        [StringLength(50)]
        public string Departamento { get; set; }
        public int Id { get; internal set; }
    }
}
