using Deneme1.Models;
using Microsoft.EntityFrameworkCore;

namespace Deneme1.Entities
{
    public class DBContext : DbContext

    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {

            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=OkulDB;TrustServerCertificate=True");

        }

        public DbSet<Ogrenci> Ogrenci { get; set; }



    }
}
