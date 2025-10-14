namespace FacturacionElectronicaSV.ViewModels
{
    public class FacturaViewModel
    {
        public int IdCliente { get; set; }
        public string NumeroControl { get; set; }
        public string FormaPago { get; set; }
        public string Contrasena { get; set; }

        public decimal SubTotal { get; set; }
        public decimal TotalGravada { get; set; }
        public decimal TotalIVA { get; set; }
        public decimal TotalPagar { get; set; }
        public string TotalLetras { get; set; }

        public List<ItemFacturaViewModel> Detalles { get; set; } = new();
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

}
