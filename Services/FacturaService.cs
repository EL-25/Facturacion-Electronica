using FacturacionElectronicaSV.Models;
using FacturacionElectronicaSV.Models.DTE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FacturacionElectronicaSV.Services
{
    public interface IFacturaService
    {
        FacturaDTE ConstruirFacturaDTE(Documento doc, List<DetalleDocumento> detalles, Receptor receptor, Emisor emisor);
    }

    public class FacturaService : IFacturaService
    {
        public FacturaDTE ConstruirFacturaDTE(Documento doc, List<DetalleDocumento> detalles, Receptor receptor, Emisor emisor)
        {
            return new FacturaDTE
            {
                identificacion = new Identificacion
                {
                    ambiente = "01",
                    codigoGeneracion = doc.CodigoGeneracion.ToString().ToUpper(),
                    fecEmi = doc.FechaEmision.ToString("yyyy-MM-dd"),
                    horEmi = doc.FechaEmision.ToString("HH:mm:ss"),
                    numeroControl = doc.NumeroControl,
                    tipoDte = doc.TipoDTE,
                    tipoModelo = "1",
                    tipoOperacion = "1",
                    tipoMoneda = "USD",
                    version = 1
                },
                emisor = new EmisorDTE
                {
                    nit = emisor.NIT,
                    nrc = emisor.NRC,
                    codActividad = emisor.CodActividad,
                    descActividad = emisor.DescActividad,
                    nombreComercial = emisor.NombreComercial,
                    tipoEstablecimiento = emisor.TipoEstablecimiento,
                    direccion = new Direccion
                    {
                        complemento = emisor.Complemento,
                        departamento = emisor.Departamento,
                        municipio = emisor.Municipio
                    },
                    telefono = emisor.Telefono,
                    correo = emisor.Correo,
                    nombre = emisor.Nombre
                },
                receptor = new ReceptorDTE
                {
                    codActividad = receptor.CodActividad,
                    correo = receptor.Correo,
                    descActividad = receptor.DescActividad,
                    direccion = new Direccion
                    {
                        complemento = receptor.Complemento,
                        departamento = receptor.Departamento,
                        municipio = receptor.Municipio
                    },
                    nombre = receptor.Nombre,
                    numDocumento = receptor.NumeroDocumento,
                    tipoDocumento = receptor.TipoDocumento
                },
                cuerpoDocumento = detalles.Select((d, i) => new ItemDTE
                {
                    cantidad = d.Cantidad,
                    codigo = d.Codigo,
                    descripcion = d.Descripcion,
                    precioUni = d.PrecioUnitario,
                    ventaGravada = d.MontoTotal,
                    ivaItem = d.IVA,
                    numItem = i + 1,
                    tipoItem = d.TipoItem,
                    uniMedida = d.UnidadMedida
                }).ToList(),
                resumen = new ResumenDTE
                {
                    condicionOperacion = 1,
                    montoTotalOperacion = doc.TotalPagar,
                    subTotal = doc.SubTotal,
                    totalGravada = doc.TotalGravada,
                    totalIva = doc.TotalIVA,
                    totalPagar = doc.TotalPagar,
                    totalLetras = doc.TotalLetras,
                    pagos = new List<Pago>
                    {
                        new Pago
                        {
                            codigo = doc.FormaPago,
                            montoPago = doc.TotalPagar,
                            periodo = 15,
                            plazo = "01"
                        }
                    }
                }
            };
        }
    }
}
