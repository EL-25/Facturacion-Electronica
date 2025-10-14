using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionElectronicaSV.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [StringLength(100)]
        public string Clave { get; set; }

        // Datos fiscales requeridos por Hacienda
        [Required(ErrorMessage = "El NIT es obligatorio.")]
        [StringLength(14)]
        public string NIT { get; set; }

        [Required(ErrorMessage = "El NRC es obligatorio.")]
        [StringLength(10)]
        public string NRC { get; set; }

        [Required(ErrorMessage = "El código de actividad es obligatorio.")]
        [StringLength(10)]
        public string CodActividad { get; set; }

        [Required(ErrorMessage = "La descripción de la actividad es obligatoria.")]
        [StringLength(100)]
        public string DescActividad { get; set; }

        [Required(ErrorMessage = "El nombre comercial es obligatorio.")]
        [StringLength(100)]
        public string NombreComercial { get; set; }

        [Required(ErrorMessage = "El tipo de establecimiento es obligatorio.")]
        [StringLength(10)]
        public string TipoEstablecimiento { get; set; }

        [StringLength(200)]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "El departamento es obligatorio.")]
        [StringLength(50)]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "El municipio es obligatorio.")]
        [StringLength(50)]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [StringLength(100)]
        public string Correo { get; set; }
    }
}
