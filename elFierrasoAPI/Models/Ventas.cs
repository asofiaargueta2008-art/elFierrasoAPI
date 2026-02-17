using System.ComponentModel.DataAnnotations;

namespace elFierrasoAPI.Models
{
    public class Ventas
    {
        [Key]
        public int idVenta { get; set; }
        public DateTime fecha { get; set; }
        public int cantidadVendida { get; set; }
        public decimal total { get; set; }
        public int idProducto { get; set; }
    }
}
