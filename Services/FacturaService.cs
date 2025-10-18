using FacturacionElectronicaSV.Models;
using FacturacionElectronicaSV.Models.DTE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.Json;

namespace FacturacionElectronicaSV.Services
{
    public interface IFacturaService
    {
        FacturaDTE ConstruirFacturaDTE(Documento doc, List<DetalleDocumento> detalles, Receptor receptor, Emisor emisor);
        byte[] GenerarPDF(Documento documento, List<DetalleDocumento> detalles, Receptor receptor, Emisor emisor);
        string GenerarJson(Documento documento, List<DetalleDocumento> detalles, Receptor receptor, Emisor emisor);
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
                    condicionOperacion = doc.FormaPago == "01" ? 1 : 2,
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

        public byte[] GenerarPDF(Documento documento, List<DetalleDocumento> detalles, Receptor receptor, Emisor emisor)
        {
            using (var ms = new MemoryStream())
            {
                var doc = new Document(PageSize.A4, 40f, 40f, 40f, 40f);
                var writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                // Encabezado
                doc.Add(new Paragraph("DigiFactura SV", fontHeader));
                doc.Add(new Paragraph($"DTE Nº: {documento.NumeroControl}", fontNormal));
                doc.Add(new Paragraph($"Código Generación: {documento.CodigoGeneracion}", fontNormal));
                doc.Add(new Paragraph($"Fecha Generación: {documento.FechaEmision:dd/MM/yyyy, HH:mm}", fontNormal));
                doc.Add(new Paragraph($"Forma de Pago: {documento.FormaPago}", fontNormal));
                doc.Add(new Paragraph(" "));

                // Emisor
                doc.Add(new Paragraph("EMISOR", fontHeader));
                doc.Add(new Paragraph($"Nombre o Razón Social: {emisor.Nombre}", fontNormal));
                doc.Add(new Paragraph($"Dirección: {emisor.Complemento}, {emisor.Municipio}, {emisor.Departamento}", fontNormal));
                doc.Add(new Paragraph($"Correo electrónico: {emisor.Correo}", fontNormal));
                doc.Add(new Paragraph($"Teléfono: {emisor.Telefono}", fontNormal));
                doc.Add(new Paragraph($"NRC: {emisor.NRC}", fontNormal));
                doc.Add(new Paragraph($"Actividad Económica: {emisor.DescActividad}", fontNormal));
                doc.Add(new Paragraph(" "));

                // Receptor
                doc.Add(new Paragraph("RECEPTOR", fontHeader));
                doc.Add(new Paragraph($"Nombre o Razón Social: {receptor.Nombre}", fontNormal));
                doc.Add(new Paragraph($"Documento: {receptor.TipoDocumento} - {receptor.NumeroDocumento}", fontNormal));
                doc.Add(new Paragraph($"Dirección: {receptor.Complemento}, {receptor.Municipio}, {receptor.Departamento}", fontNormal));
                doc.Add(new Paragraph($"Correo electrónico: {receptor.Correo}", fontNormal));
                doc.Add(new Paragraph($"Teléfono: {receptor.Telefono}", fontNormal));
                doc.Add(new Paragraph(" "));

                // Detalle de ítems
                var table = new PdfPTable(5) { WidthPercentage = 100 };
                table.SetWidths(new float[] { 40f, 20f, 20f, 20f, 20f });

                table.AddCell("Descripción");
                table.AddCell("Cantidad");
                table.AddCell("Precio");
                table.AddCell("Gravado");
                table.AddCell("IVA");

                foreach (var item in detalles)
                {
                    table.AddCell(item.Descripcion);
                    table.AddCell(item.Cantidad.ToString());
                    table.AddCell($"${item.PrecioUnitario:F2}");
                    table.AddCell($"${item.MontoTotal:F2}");
                    table.AddCell($"${item.IVA:F2}");
                }

                doc.Add(table);
                doc.Add(new Paragraph(" "));

                // Totales
                doc.Add(new Paragraph($"Subtotal: ${documento.SubTotal:F2}", fontNormal));
                doc.Add(new Paragraph($"Total Gravado: ${documento.TotalGravada:F2}", fontNormal));
                doc.Add(new Paragraph($"IVA: ${documento.TotalIVA:F2}", fontNormal));
                doc.Add(new Paragraph($"Total a Pagar: ${documento.TotalPagar:F2}", fontNormal));
                doc.Add(new Paragraph($"Valor en letras: {documento.TotalLetras}", fontNormal));
                doc.Add(new Paragraph(" "));

                // Condición de pago
                var condicion = documento.FormaPago == "01" ? "1 - CONTADO" : "2 - CRÉDITO";
                doc.Add(new Paragraph($"Condición de Pago: {condicion}", fontNormal));

                doc.Close();
                return ms.ToArray();
            }
        }



        public string GenerarJson(Documento documento, List<DetalleDocumento> detalles, Receptor receptor, Emisor emisor)
        {
            var facturaDTE = ConstruirFacturaDTE(documento, detalles, receptor, emisor);
            return JsonSerializer.Serialize(facturaDTE, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
        }
    }
}
