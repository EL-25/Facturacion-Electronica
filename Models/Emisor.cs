using System.ComponentModel.DataAnnotations;

namespace FacturacionElectronicaSV.Models
{
    public class Emisor
    {
        [Key]
        public int IdEmisor { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreComercial { get; set; }

        [Required]
        [StringLength(14)]
        public string NIT { get; set; }

        [StringLength(10)]
        public string NRC { get; set; }

        [StringLength(100)]
        public string ActividadEconomica { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(50)]
        public string Municipio { get; set; }

        [StringLength(50)]
        public string Departamento { get; set; }

        [StringLength(10)]
        public string CodigoEstablecimiento { get; set; }

        [StringLength(10)]
        public string CodigoSucursal { get; set; }
    }
}