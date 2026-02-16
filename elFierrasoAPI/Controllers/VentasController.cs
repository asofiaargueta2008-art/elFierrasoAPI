using Microsoft.AspNetCore.Mvc;
using elFierrasoAPI.Data;
using elFierrasoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace elFierrasoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly elFierrasoDbContext _context;
        public VentasController(elFierrasoDbContext context)
        {
            _context = context;
        }

        // GET: api/Ventas
        [HttpGet]
        public IActionResult GetVentas()
        {
            var ventas = _context.Ventas.ToList();
            return Ok(ventas);
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public IActionResult GetVenta(int id)
        {
            var venta = _context.Ventas.Find(id);
            if (venta == null)
            {
                return NotFound();
            }
            return Ok(venta);
        }

        // POST: api/Ventas
        [HttpPost]
        public IActionResult CreateVenta(Ventas venta)
        {
            _context.Ventas.Add(venta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetVenta), new { id = venta.idVenta }, venta);
        }

    }
}
