using System.ComponentModel.DataAnnotations;

namespace FacturacionElectronicaSV.ViewModels
{
    public class FacturaViewModel
    {
        public int CondicionOperacion { get; set; } // contado y crédito

        public string FechaDTE { get; set; }
        public string HoraDTE { get; set; }

        // Número DTE generado por backend (nuevo)
        public string NumeroDTE { get; set; }

        // Datos generales
        public int IdCliente { get; set; }
        public string NumeroControl { get; set; }
        public string FormaPago { get; set; }
        public string Contrasena { get; set; }

        // Totales
        public decimal SubTotal { get; set; }
        public decimal TotalGravada { get; set; }
        public decimal TotalIVA { get; set; }
        public decimal TotalPagar { get; set; }
        public string TotalLetras { get; set; }

        // Detalle de ítems
        public List<ItemFacturaViewModel> Detalles { get; set; } = new();

        // Datos del emisor (autocompletados y editables)
        public EmisorViewModel Emisor { get; set; } = new();

        // Datos del receptor (seleccionado o ingresado)
        public ReceptorViewModel Receptor { get; set; } = new();
    }

    public class ItemFacturaViewModel
    {
        public int Cantidad { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal VentaGravada { get; set; }
        public decimal IVA { get; set; }
        public int TipoItem { get; set; }
        public int UnidadMedida { get; set; }
    }

    public class EmisorViewModel
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; } // si lo usás
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Complemento { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        
        public string NIT { get; set; }
        public string NRC { get; set; }
        public string CodActividad { get; set; }
        public string DescActividad { get; set; }
        public string NombreComercial { get; set; }
        public string TipoEstablecimiento { get; set; }
    }


    public class ReceptorViewModel
    {
        [Required]
        public string TipoDocumento { get; set; }

        [Required]
        public string NumeroDocumento { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Departamento { get; set; }

        [Required]
        public string Municipio { get; set; }

        public string Complemento { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefono { get; set; }
    }
}
