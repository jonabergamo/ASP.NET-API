using Microsoft.EntityFrameworkCore;
using SegundaAPI.Model;

namespace SegundaAPI.Infra
{
    public class ConnectionContext : DbContext

    {
        public DbSet<Employee> Employees { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
           "Server=localhost;" +
           "Port=5432;" +
           "Database=cesharp;" +
           "User Id=postgres;" +
           "Password=12345678;"
        );
        }
    }
}
