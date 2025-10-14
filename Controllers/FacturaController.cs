using FacturacionElectronicaSV.Data;
using FacturacionElectronicaSV.Models;
using FacturacionElectronicaSV.Services;
using FacturacionElectronicaSV.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.StaticFiles;

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

            var emisor = _context.Emisor.FirstOrDefault(); // ← se asume único emisor

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

        // POST: /Factura/Generar
        [HttpPost]
        public IActionResult Generar(FacturaViewModel model)
        {
            // Validación de contraseña
            if (string.IsNullOrWhiteSpace(model.Contrasena) || model.Contrasena != "1234") // ← reemplazá por tu lógica real
            {
                ModelState.AddModelError("Contrasena", "Contraseña incorrecta.");
                ViewBag.Clientes = _context.Receptores.ToList();
                ViewBag.FormasPago = new[] {
                    new { Codigo = "01", Nombre = "Contado" },
                    new { Codigo = "02", Nombre = "Crédito" }
                };
                return View("Crear", model);
            }

            var emisorDb = _context.Emisor.FirstOrDefault();
            if (emisorDb == null)
            {
                ModelState.AddModelError("", "No se encontró información del emisor.");
                ViewBag.Clientes = _context.Receptores.ToList();
                ViewBag.FormasPago = new[] {
                    new { Codigo = "01", Nombre = "Contado" },
                    new { Codigo = "02", Nombre = "Crédito" }
                };
                return View("Crear", model);
            }

            var receptor = _context.Receptores.FirstOrDefault(c => c.IdReceptor == model.IdCliente);
            if (receptor == null)
            {
                ModelState.AddModelError("IdCliente", "Cliente no válido.");
                ViewBag.Clientes = _context.Receptores.ToList();
                ViewBag.FormasPago = new[] {
                    new { Codigo = "01", Nombre = "Contado" },
                    new { Codigo = "02", Nombre = "Crédito" }
                };
                return View("Crear", model);
            }

            var documento = new Documento
            {
                FechaEmision = DateTime.Now,
                NumeroControl = model.NumeroControl,
                TipoDTE = "01",
                CodigoGeneracion = Guid.NewGuid(),
                FormaPago = model.FormaPago,
                SubTotal = model.SubTotal,
                TotalGravada = model.TotalGravada,
                TotalIVA = model.TotalIVA,
                TotalPagar = model.TotalPagar,
                TotalLetras = model.TotalLetras,
                IdEmisor = emisorDb.IdEmisor,
                IdReceptor = receptor.IdReceptor
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

            var emisorEditable = new Emisor
            {
                TipoDocumento = model.Emisor.TipoDocumento,
                Nombre = model.Emisor.Nombre,
                Departamento = model.Emisor.Departamento,
                Municipio = model.Emisor.Municipio,
                Complemento = model.Emisor.Complemento,
                Correo = model.Emisor.Correo,
                Telefono = model.Emisor.Telefono
            };

            var facturaDTE = _facturaService.ConstruirFacturaDTE(documento, detalles, receptor, emisorEditable);

            var json = JsonSerializer.Serialize(facturaDTE, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            // Guardar JSON en TempData para confirmación visual
            TempData["JsonGenerado"] = json;

            // Generar PDF (estructura lista para integrar)
            var pdfBytes = _facturaService.GenerarPDF(documento, detalles, receptor, emisorEditable); // ← implementalo en tu servicio

            // Guardar PDF temporalmente si querés mostrarlo o enviarlo

            // Redirigir a descarga directa del JSON
            return File(Encoding.UTF8.GetBytes(json), "application/json", $"DTE_{documento.NumeroControl}.json");
        }

        // GET: /Factura/Confirmacion
        public IActionResult Confirmacion()
        {
            ViewBag.Json = TempData["JsonGenerado"];
            return View();
        }
    }
}
