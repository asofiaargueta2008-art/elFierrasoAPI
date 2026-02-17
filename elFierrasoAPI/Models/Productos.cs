
using System.ComponentModel.DataAnnotations;

namespace elFierrasoAPI.Models
{
    public class Productos
    {
        [Key]
        public int idProducto { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public string? urlImagen { get; set; }
        public int stock { get; set; }
        public int idProveedor { get; set; }
    }
}