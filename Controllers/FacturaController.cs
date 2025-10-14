using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FacturacionElectronicaSV.Data;
using FacturacionElectronicaSV.Models;
using FacturacionElectronicaSV.ViewModels;
using FacturacionElectronicaSV.Services;
using System.Text.Json;
using System.Linq;
using System;

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

            return View(new FacturaViewModel());
        }

        // POST: /Factura/Generar
        [HttpPost]
        public IActionResult Generar(FacturaViewModel model)
        {
            var emisor = _context.Emisor.FirstOrDefault(); // ← se asume único emisor
            if (emisor == null)
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
                IdEmisor = emisor.IdEmisor,
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

            var facturaDTE = _facturaService.ConstruirFacturaDTE(documento, detalles, receptor, emisor);

            var json = JsonSerializer.Serialize(facturaDTE, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            TempData["JsonGenerado"] = json;
            return RedirectToAction("Confirmacion");
        }

        // GET: /Factura/Confirmacion
        public IActionResult Confirmacion()
        {
            ViewBag.Json = TempData["JsonGenerado"];
            return View();
        }
    }
}
