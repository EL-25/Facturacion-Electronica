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

        // GET: /Factura/Confirmacion
        public IActionResult Confirmacion()
        {
            TempData.Keep();

            ViewBag.Json = TempData["JsonGenerado"];
            ViewBag.JsonNombre = TempData["JsonNombre"];
            ViewBag.PdfNombre = TempData["PdfNombre"];
            return View();
        }

        // GET: /Factura/DescargarJson
        public IActionResult DescargarJson()
        {
            TempData.Keep();

            var json = TempData["JsonGenerado"] as string;
            var nombre = TempData["JsonNombre"] as string ?? "DTE.json";

            if (string.IsNullOrEmpty(json))
                return RedirectToAction("Crear");

            var bytes = Encoding.UTF8.GetBytes(json);
            return File(bytes, "application/json", nombre);
        }

        // GET: /Factura/DescargarPDF
        public IActionResult DescargarPDF()
        {
            TempData.Keep();

            var pdfBase64 = TempData["PdfGenerado"] as string;
            var nombre = TempData["PdfNombre"] as string ?? "DTE.pdf";

            if (string.IsNullOrEmpty(pdfBase64))
                return RedirectToAction("Crear");

            var bytes = Convert.FromBase64String(pdfBase64);
            return File(bytes, "application/pdf", nombre);
        }
    }
}
