using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [StringLength(10)]
        public string TipoDocumento { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }

        [StringLength(10)]
        public string? CodActividad { get; set; }

        [StringLength(100)]
        public string? DescActividad { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }

        [StringLength(200)]
        public string Complemento { get; set; }

        [StringLength(50)]
        public string Municipio { get; set; }

        [StringLength(50)]
        public string Departamento { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Ingresa un número de teléfono válido.")]
        [StringLength(15, ErrorMessage = "El teléfono debe tener máximo 15 caracteres.")]
        public string Telefono { get; set; }
    }
}
