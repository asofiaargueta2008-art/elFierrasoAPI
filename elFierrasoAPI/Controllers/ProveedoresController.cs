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
            if (proveedor == null) return BadRequest();

            _context.Proveedores.Add(proveedor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.idProveedor }, proveedor);
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public IActionResult UpdateProveedor(int id, [FromBody] Proveedores proveedor)
        {
            if (id != proveedor.idProveedor)
                return BadRequest(new { message = "El id de la URL no coincide con el id del body." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProveedor = _context.Proveedores.Find(id);
            if (existingProveedor == null)
                return NotFound(new { message = "Proveedor no encontrado." });

            existingProveedor.nombre = proveedor.nombre;
            existingProveedor.telefono = proveedor.telefono;
            existingProveedor.email = proveedor.email;

            _context.SaveChanges();
            return NoContent();
        }

        // PATCH: api/Proveedores/5
        [HttpPatch("{id}")]
        public IActionResult PatchProveedor(int id, Proveedores proveedor)
        {
            var existingProveedor = _context.Proveedores.Find(id);
            if (existingProveedor == null) 
                return NotFound(new { message = "Proveedor no encontrado." });
            
            if (!string.IsNullOrEmpty(proveedor.nombre)) 
                existingProveedor.nombre = proveedor.nombre;

            if (proveedor.telefono != 0)
            {
                if (proveedor.telefono <= 9999999)
                    return BadRequest(new { message = "Teléfono inválido (debe tener al menos 8 dígitos)." });

                existingProveedor.telefono = proveedor.telefono;
            }

            if (!string.IsNullOrEmpty(proveedor.email)) 
                existingProveedor.email = proveedor.email;

            _context.SaveChanges();
            return Ok(existingProveedor);
        }

        //DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProveedor(int id)
        {
            var proveedor = _context.Proveedores.Find(id);
            if (proveedor == null) 
                return NotFound(new { message = "Proveedor no encontrado." });

            _context.Proveedores.Remove(proveedor);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
