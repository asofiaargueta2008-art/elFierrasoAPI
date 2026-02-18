using Microsoft.AspNetCore.Mvc;
using elFierrasoAPI.Data;
using elFierrasoAPI.Models;
using System.Linq;

namespace elFierrasoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly elFierrasoDbContext _context;

        public ProductosController(elFierrasoDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public IActionResult GetProductos()
        {
            var productos = _context.Productos
                .Where(p => p.activo == true)
                .ToList();

            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public IActionResult GetProducto(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto == null || producto.activo == false)
                return NotFound(new { message = "Producto no encontrado." });

            return Ok(producto);
        }

        // POST: api/Productos
        [HttpPost]
        public IActionResult CreateProducto([FromBody] Productos producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var proveedorExiste = _context.Proveedores.Any(p => p.idProveedor == producto.idProveedor);
            if (!proveedorExiste)
                return BadRequest(new { message = "El idProveedor no existe." });

            producto.activo = true;

            _context.Productos.Add(producto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.idProducto }, producto);
        }

        // PUT: api/Productos
        [HttpPut("{id}")]
        public IActionResult UpdateProducto(int id, [FromBody] Productos producto)
        {
            if (id != producto.idProducto)
                return BadRequest(new { message = "El id no coincide." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProducto = _context.Productos.Find(id);
            if (existingProducto == null || existingProducto.activo == false)
                return NotFound(new { message = "Producto no encontrado." });

            var proveedorExiste = _context.Proveedores.Any(p => p.idProveedor == producto.idProveedor);
            if (!proveedorExiste)
                return BadRequest(new { message = "El idProveedor no existe." });

            existingProducto.nombre = producto.nombre;
            existingProducto.precio = producto.precio;
            existingProducto.stock = producto.stock;
            existingProducto.urlImagen = producto.urlImagen;
            existingProducto.idProveedor = producto.idProveedor;

            _context.SaveChanges();
            return NoContent();
        }

        // PATCH: api/Productos/5 
        [HttpPatch("{id}")]
        public IActionResult UpdateProductoPartial(int id, [FromBody] Productos producto)
        {
            var existingProducto = _context.Productos.Find(id);
            if (existingProducto == null || existingProducto.activo == false)
                return NotFound(new { message = "Producto no encontrado." });

            if (!string.IsNullOrWhiteSpace(producto.nombre))
                existingProducto.nombre = producto.nombre;

            if (producto.precio > 0)
                existingProducto.precio = producto.precio;
            else if (producto.precio < 0)
                return BadRequest(new { message = "El precio no puede ser negativo." });

            if (producto.stock != 0)
            {
                if (producto.stock < 0)
                    return BadRequest(new { message = "El stock no puede ser negativo." });

                existingProducto.stock = producto.stock;
            }

            if (!string.IsNullOrWhiteSpace(producto.urlImagen))
                existingProducto.urlImagen = producto.urlImagen;

            if (producto.idProveedor > 0)
            {
                var proveedorExiste = _context.Proveedores.Any(p => p.idProveedor == producto.idProveedor);
                if (!proveedorExiste)
                    return BadRequest(new { message = "El idProveedor no existe." });

                existingProducto.idProveedor = producto.idProveedor;
            }
            else if (producto.idProveedor < 0)
            {
                return BadRequest(new { message = "idProveedor inválido." });
            }

            _context.SaveChanges();
            return Ok(existingProducto);
        }

        // DELETE: api/Productos
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null || producto.activo == false)
                return NotFound(new { message = "Producto no encontrado." });

            producto.activo = false;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
