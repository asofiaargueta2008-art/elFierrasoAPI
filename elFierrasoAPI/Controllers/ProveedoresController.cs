using Microsoft.AspNetCore.Mvc;
using elFierrasoAPI.Data;
using elFierrasoAPI.Models;

namespace elFierrasoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly elFierrasoDbContext _context;
        public ProveedoresController(elFierrasoDbContext context)
        {
            _context = context;
        }

        // GET: api/Proveedores
        [HttpGet]
        public IActionResult GetProveedores()
        {
            var proveedores = _context.Proveedores.ToList();
            return Ok(proveedores);
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public IActionResult GetProveedor(int id)
        {
            var proveedor = _context.Proveedores.Find(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return Ok(proveedor);
        }

        // POST: api/Proveedores
        [HttpPost]
        public IActionResult CreateProveedor(Proveedores proveedor)
        {
            _context.Proveedores.Add(proveedor);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.idProveedor }, proveedor);
        }
    }
}
