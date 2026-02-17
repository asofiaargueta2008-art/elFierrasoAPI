
using System.ComponentModel.DataAnnotations;

namespace elFierrasoAPI.Models
{
    public class Proveedores
    {
        [Key]
        public int idProveedor { get; set; }
        public string nombre { get; set; } = string.Empty;
        public int telefono { get; set; }
        public string email { get; set; } = string.Empty;
    }
}
