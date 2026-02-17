using Microsoft.AspNetCore.Mvc;
using elFierrasoAPI.Data;
using elFierrasoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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
        public IActionResult CreateVenta([FromBody]Ventas venta)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (venta.cantidadVendida <= 0)
                return BadRequest(new { message = "La cantidad vendida debe ser mayor a 0." });

            try
            {
                _context.Database.ExecuteSqlRaw(
                     "EXEC dbo.RegistrarVenta @idProducto, @cantidadVendida",
                     new SqlParameter("@idProducto", venta.idProducto),
                     new SqlParameter("@cantidadVendida", venta.cantidadVendida)
                 );


                // devolver algo útil: la última venta creada para ese producto (opcional)
                var ultimaVenta = _context.Ventas
                    .OrderByDescending(v => v.idVenta)
                    .FirstOrDefault();

                return Ok(new { message = "Venta registrada y stock actualizado.", venta = ultimaVenta });
            }
            catch (Exception ex)
            {
                // Si querés fino: revisar ex.InnerException.Message para "Stock insuficiente"
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

    }
}
