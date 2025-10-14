using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using FacturacionElectronicaSV.Data;
using FacturacionElectronicaSV.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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
        public async Task<IActionResult> Login(string nombreUsuario, string clave)
        {
            var usuario = _context.Usuario
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null)
            {
                ViewBag.Error = "El usuario no existe.";
                return View();
            }

            if (usuario.Clave != clave)
            {
                ViewBag.Error = "La contraseña es incorrecta.";
                return View();
            }

            // Crear claims para el usuario
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim("NombreUsuario", usuario.NombreUsuario)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Crear cookie de autenticación
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Documento");
        }

        // GET: /Cuenta/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Cuenta");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            _context.Usuario.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

    }
}
