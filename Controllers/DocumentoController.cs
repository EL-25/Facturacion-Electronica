using FacturacionElectronicaSV.Data;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionElectronicaSV.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var emisor = _context.Emisor.FirstOrDefault(); // o filtrado por usuario si aplica
            ViewBag.NIT = emisor?.NIT ?? "N/D";
            ViewBag.NRC = emisor?.NRC ?? "N/D";
            ViewBag.Usuario = User.Identity?.Name ?? "Usuario";

            return View();
        }
    }

}
