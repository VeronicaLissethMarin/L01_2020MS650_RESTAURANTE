using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2020MS650.Models
{
    public class restauranteContext:DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
        {
            
        }

        public DbSet<restauranteDB> restauranteDB { get; set; }
    }
}
