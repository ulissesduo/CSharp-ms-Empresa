using Microsoft.EntityFrameworkCore;
using WebAPI_Oracle.Entity;

namespace WebAPI_Oracle.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        
        public DbSet<Empresa> Empresa { get; set; }
    }
}
