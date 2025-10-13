using System.ComponentModel.DataAnnotations;

namespace FacturacionElectronicaSV.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Clave { get; set; }
    }
}