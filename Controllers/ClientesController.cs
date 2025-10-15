using FacturacionElectronicaSV.Data;
using FacturacionElectronicaSV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FacturacionElectronicaSV.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Clientes
        public IActionResult Index()
        {
            var clientes = _context.Receptores.ToList();
            return View(clientes);
        }

        // GET: /Clientes/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Clientes/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Receptor receptor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Por favor completa todos los campos obligatorios.";
                return View(receptor);
            }

            _context.Receptores.Add(receptor);
            _context.SaveChanges();

            Console.WriteLine("Cliente guardado: " + receptor.Nombre);
            TempData["Mensaje"] = "Cliente registrado correctamente.";
            return RedirectToAction("Index");
        }

        // GET: /Clientes/Editar/5
        public IActionResult Editar(int id)
        {
            var receptor = _context.Receptores.Find(id);
            if (receptor == null)
                return NotFound();

            return View(receptor);
        }

        // POST: /Clientes/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Receptor receptor)
        {
            if (!ModelState.IsValid)
                return View(receptor);

            _context.Receptores.Update(receptor);
            _context.SaveChanges();

            TempData["Mensaje"] = "Cliente actualizado correctamente.";
            return RedirectToAction("Index");
        }

        // GET: /Clientes/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            var receptor = _context.Receptores.Find(id);
            if (receptor == null)
                return NotFound();

            _context.Receptores.Remove(receptor);
            _context.SaveChanges();

            TempData["Mensaje"] = "Cliente eliminado correctamente.";
            return RedirectToAction("Index");
        }
    }
}