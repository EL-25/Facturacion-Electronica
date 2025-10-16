using FacturacionElectronicaSV.Data;
using FacturacionElectronicaSV.Models;
using FacturacionElectronicaSV.Services;
using FacturacionElectronicaSV.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace FacturacionElectronicaSV.Controllers
{
    [Authorize]
    public class FacturaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFacturaService _facturaService;

        public FacturaController(ApplicationDbContext context, IFacturaService facturaService)
        {
            _context = context;
            _facturaService = facturaService;
        }

        // GET: /Factura/Crear
        public IActionResult Crear()
        {
            ViewBag.Clientes = _context.Receptores.ToList();
            ViewBag.FormasPago = new[] {
                new { Codigo = "01", Nombre = "Contado" },
                new { Codigo = "02", Nombre = "Crédito" }
            };

            var emisor = _context.Emisor.FirstOrDefault();

            var model = new FacturaViewModel
            {
                Emisor = new EmisorViewModel
                {
                    TipoDocumento = emisor?.TipoDocumento,
                    Nombre = emisor?.Nombre,
                    Departamento = emisor?.Departamento,
                    Municipio = emisor?.Municipio,
                    Complemento = emisor?.Complemento,
                    Correo = emisor?.Correo,
                    Telefono = emisor?.Telefono
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Crear(FacturaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Convertir ViewModel a Model
            var documento = new Documento
            {
                NumeroControl = model.NumeroControl,
                FechaEmision = DateTime.Now,
                FormaPago = model.FormaPago,
                SubTotal = model.SubTotal,
                TotalGravada = model.TotalGravada,
                TotalIVA = model.TotalIVA,
                TotalPagar = model.TotalPagar,
                TotalLetras = model.TotalLetras,
                CodigoGeneracion = Guid.NewGuid(),
                TipoDTE = "01"
            };

            var detalles = model.Detalles.Select((d, i) => new DetalleDocumento
            {
                Cantidad = d.Cantidad,
                Codigo = d.Codigo,
                Descripcion = d.Descripcion,
                PrecioUnitario = d.PrecioUnitario,
                MontoTotal = d.VentaGravada,
                IVA = d.IVA,
                TipoItem = d.TipoItem,
                UnidadMedida = d.UnidadMedida
            }).ToList();

            var receptor = new Receptor
            {
                Nombre = model.Receptor.Nombre,
                Departamento = model.Receptor.Departamento,
                Municipio = model.Receptor.Municipio,
                Complemento = model.Receptor.Complemento,
                Correo = model.Receptor.Email,
                TipoDocumento = model.Receptor.TipoDocumento,
                NumeroDocumento = "00000000-0",
                CodActividad = "000",
                DescActividad = "Sin actividad registrada"
            };

            var emisor = new Emisor
            {
                Nombre = model.Emisor.Nombre,
                Departamento = model.Emisor.Departamento,
                Municipio = model.Emisor.Municipio,
                Complemento = model.Emisor.Complemento,
                Correo = model.Emisor.Correo,
                Telefono = model.Emisor.Telefono,
                TipoDocumento = model.Emisor.TipoDocumento,
                NIT = "0614-290786-101-3",
                NRC = "123456-7",
                CodActividad = "000",
                DescActividad = "Servicios informáticos",
                NombreComercial = "DigiFactura SV",
                TipoEstablecimiento = "01"
            };

            // Generar PDF y JSON
            var pdfBytes = _facturaService.GenerarPDF(documento, detalles, receptor, emisor);
            var json = _facturaService.GenerarJson(documento, detalles, receptor, emisor);

            // Usar el número de control como nombre base
            var nombreBase = documento.NumeroControl ?? "DTE";

            

            // Pasar nombres y contenido a la vista de confirmación
            TempData["JsonNombre"] = $"{nombreBase}.json";
            TempData["PdfNombre"] = $"{nombreBase}.pdf";
            TempData["JsonGenerado"] = json;

            return RedirectToAction("Confirmacion");
        }



        // POST: /Factura/Generar
        [HttpPost]
        public IActionResult Generar(string NumeroControl, string confirmarContrasena)
        {
            var nombreUsuario = User.FindFirst("NombreUsuario")?.Value;
            var usuario = _context.Usuario.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null || usuario.Clave != confirmarContrasena)
            {
                ModelState.AddModelError("", "Contraseña incorrecta.");
                return View("Crear");
            }

            TempData["NumeroControl"] = NumeroControl;
            return RedirectToAction("Confirmacion");
        }

        // GET: /Factura/Confirmacion       //esto si funciona 
        public IActionResult Confirmacion()
        {
            // Asegura que TempData sobreviva si se refresca la vista
            TempData.Keep();

            // Recuperar nombres de archivo desde TempData
            var jsonNombre = TempData["JsonNombre"] as string ?? "DTE.json";
            var pdfNombre = TempData["PdfNombre"] as string ?? "DTE.pdf";

            // Recuperar contenido JSON si lo necesitas para vista previa
            var jsonContenido = TempData["JsonGenerado"] as string;

            // Pasar datos a la vista
            ViewBag.JsonNombre = jsonNombre;
            ViewBag.PdfNombre = pdfNombre;
            ViewBag.Json = jsonContenido;

            return View();
        }


    }
}
