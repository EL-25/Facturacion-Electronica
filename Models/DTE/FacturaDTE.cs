using System;
using System.Collections.Generic;

namespace FacturacionElectronicaSV.Models.DTE
{
    public class FacturaDTE
    {
        public Identificacion identificacion { get; set; }
        public EmisorDTE emisor { get; set; }
        public ReceptorDTE receptor { get; set; }
        public List<ItemDTE> cuerpoDocumento { get; set; }
        public ResumenDTE resumen { get; set; }
    }

    public class Identificacion
    {
        public string ambiente { get; set; }
        public string tipoDte { get; set; }
        public string numeroControl { get; set; }
        public string codigoGeneracion { get; set; }
        public string tipoModelo { get; set; }
        public string tipoOperacion { get; set; }
        public string tipoMoneda { get; set; }
        public string fecEmi { get; set; }
        public string horEmi { get; set; }
        public int version { get; set; }
    }

    public class EmisorDTE
    {
        public string nit { get; set; }
        public string nrc { get; set; }
        public string nombreComercial { get; set; }
        public string tipoEstablecimiento { get; set; }
        public string codActividad { get; set; }
        public string descActividad { get; set; }
        public Direccion direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string nombre { get; set; }
    }

    public class ReceptorDTE
    {
        public string tipoDocumento { get; set; }
        public string numDocumento { get; set; }
        public string nombre { get; set; }
        public string codActividad { get; set; }
        public string descActividad { get; set; }
        public string correo { get; set; }
        public Direccion direccion { get; set; }
    }

    public class Direccion
    {
        public string complemento { get; set; }
        public string departamento { get; set; }
        public string municipio { get; set; }
    }

    public class ItemDTE
    {
        public int numItem { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public decimal precioUni { get; set; }
        public decimal ventaGravada { get; set; }
        public decimal ivaItem { get; set; }
        public int tipoItem { get; set; }
        public int uniMedida { get; set; }
    }

    public class ResumenDTE
    {
        public int condicionOperacion { get; set; }
        public decimal subTotal { get; set; }
        public decimal totalGravada { get; set; }
        public decimal totalIva { get; set; }
        public decimal totalPagar { get; set; }
        public string totalLetras { get; set; }
        public decimal montoTotalOperacion { get; set; }
        public List<Pago> pagos { get; set; }
    }

    public class Pago
    {
        public string codigo { get; set; }
        public decimal montoPago { get; set; }
        public int periodo { get; set; }
        public string plazo { get; set; }
    }
}
