
using System.ComponentModel.DataAnnotations;

namespace elFierrasoAPI.Models
{
    public class Proveedores
    {
        [Key]
        public int idProveedor { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}
