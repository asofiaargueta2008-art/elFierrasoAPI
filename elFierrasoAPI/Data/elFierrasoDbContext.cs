using Microsoft.EntityFrameworkCore;
using elFierrasoAPI.Models;

namespace elFierrasoAPI.Data
{
    public class elFierrasoDbContext : DbContext
    {
        public elFierrasoDbContext(DbContextOptions<elFierrasoDbContext> options) : base(options) { }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
    }
}
