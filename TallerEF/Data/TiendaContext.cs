using Microsoft.EntityFrameworkCore;
using TallerEF.Models;

namespace TallerEF.Data
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
    }
}