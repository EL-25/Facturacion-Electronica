using FacturacionElectronicaSV.Data;
using FacturacionElectronicaSV.Models;
using FacturacionElectronicaSV.Services;
using FacturacionElectronicaSV.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", $"{nombreBase}.json");
            System.IO.File.WriteAllText(ruta, json);

            var rutaPdf = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", $"{nombreBase}.pdf");
            System.IO.File.WriteAllBytes(rutaPdf, pdfBytes);

            Console.WriteLine($"✅ JSON guardado en: {ruta}");
            Console.WriteLine($"✅ PDF guardado en: {rutaPdf}");

            if (!System.IO.File.Exists(ruta))
            {
                Console.WriteLine($"❌ El archivo no se guardó correctamente en: {ruta}");
            }

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

            // Simulación mínima para generar el JSON
            var documento = new Documento
            {
                NumeroControl = NumeroControl,
                FechaEmision = DateTime.Now,
                FormaPago = "01",
                SubTotal = 100.00m,
                TotalGravada = 100.00m,
                TotalIVA = 13.00m,
                TotalPagar = 113.00m,
                TotalLetras = "Ciento trece dólares",
                CodigoGeneracion = Guid.NewGuid(),
                TipoDTE = "01"
            };

            var detalles = new List<DetalleDocumento>
            {
                new DetalleDocumento
                {
                    Cantidad = 1,
                    Codigo = "AUTO",
                    Descripcion = "Producto genérico",
                    PrecioUnitario = 100.00m,
                    MontoTotal = 100.00m,
                    IVA = 13.00m,
                    TipoItem = 1,
                    UnidadMedida = 59
                }
            };

            var receptor = _context.Receptores.FirstOrDefault() ?? new Receptor
            {
                Nombre = "Receptor genérico",
                Departamento = "San Salvador",
                Municipio = "San Salvador",
                Complemento = "Centro",
                Correo = "receptor@correo.com",
                TipoDocumento = "DUI",
                NumeroDocumento = "00000000-0",
                CodActividad = "000",
                DescActividad = "Sin actividad registrada"
            };

            var emisor = _context.Emisor.FirstOrDefault() ?? new Emisor
            {
                Nombre = "DigiFactura SV",
                Departamento = "San Salvador",
                Municipio = "San Salvador",
                Complemento = "Centro",
                Correo = "emisor@correo.com",
                Telefono = "2222-2222",
                TipoDocumento = "NIT",
                NIT = "0614-290786-101-3",
                NRC = "123456-7",
                CodActividad = "000",
                DescActividad = "Servicios informáticos",
                NombreComercial = "DigiFactura SV",
                TipoEstablecimiento = "01"
            };

            var json = _facturaService.GenerarJson(documento, detalles, receptor, emisor);
            var pdfBytes = _facturaService.GenerarPDF(documento, detalles, receptor, emisor);

            var nombreBase = $"{NumeroControl}_{DateTime.Now:yyyyMMdd_HHmmss}";
            var carpetaFiles = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");
            if (!Directory.Exists(carpetaFiles)) Directory.CreateDirectory(carpetaFiles);

            var ruta = Path.Combine(carpetaFiles, $"{nombreBase}.json");
            System.IO.File.WriteAllText(ruta, json);

            var rutaPdf = Path.Combine(carpetaFiles, $"{nombreBase}.pdf");
            System.IO.File.WriteAllBytes(rutaPdf, pdfBytes);


            Console.WriteLine($"✅ JSON guardado en: {ruta}");
            Console.WriteLine($"✅ PDF guardado en: {rutaPdf}");

            TempData["JsonNombre"] = $"{nombreBase}.json";
            TempData["PdfNombre"] = $"{nombreBase}.pdf";
            TempData["JsonGenerado"] = json;

            return RedirectToAction("Confirmacion");
        }

        // GET: /Factura/Confirmacion
        public IActionResult Confirmacion()
        {
            TempData.Keep();

            var jsonNombre = TempData["JsonNombre"] as string ?? "DTE.json";
            var pdfNombre = TempData["PdfNombre"] as string ?? "DTE.pdf";
            var jsonContenido = TempData["JsonGenerado"] as string;

            ViewBag.JsonNombre = jsonNombre;
            ViewBag.PdfNombre = pdfNombre;
            ViewBag.Json = jsonContenido;

            return View();
        }
    }
}