namespace FacturacionElectronicaSV.ViewModels
{
    public class FacturaViewModel
    {
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
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Complemento { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    }

    public class ReceptorViewModel
    {
        public string TipoDocumento { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}

