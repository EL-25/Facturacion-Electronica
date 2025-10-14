using System.ComponentModel.DataAnnotations;

namespace FacturacionElectronicaSV.Models
{
    public class Emisor
    {
        [Key]
        public int IdEmisor { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } // ← Nombre legal del emisor

        [Required]
        [StringLength(100)]
        public string NombreComercial { get; set; } // ← Nombre visible en la factura

        [Required]
        [StringLength(14)]
        public string NIT { get; set; }

        [StringLength(10)]
        public string NRC { get; set; }

        [StringLength(10)]
        public string CodActividad { get; set; }

        [StringLength(100)]
        public string DescActividad { get; set; }

        [StringLength(10)]
        public string TipoEstablecimiento { get; set; }

        [StringLength(200)]
        public string Complemento { get; set; }

        [StringLength(50)]
        public string Departamento { get; set; }

        [StringLength(50)]
        public string Municipio { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }
    }
}
