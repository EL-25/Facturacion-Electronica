using Microsoft.AspNetCore.Mvc;
using FacturacionElectronicaSV.Data;
using FacturacionElectronicaSV.Models;
using System.Linq;

namespace FacturacionElectronicaSV.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Cuenta/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Cuenta/Login
        [HttpPost]
        public IActionResult Login(string nombreUsuario, string clave)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.Clave == clave);

            if (usuario != null)
            {
                // Aqui se podria guardar sesión si queremos
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }
    }
}