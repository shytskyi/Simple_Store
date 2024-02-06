using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext           // todo: implemented EF_Core in the future 
    {
        //private const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Simple_Store;Trusted_Connection=True";
        //public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(connectionString);

            //var config = new ConfigurationBuilder()                                             
            //            .AddJsonFile("appsettings.json")                                        
            //            .SetBasePath(Directory.GetCurrentDirectory())                           
            //            .Build();                                                                                                                                                        
            //optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                          
        }
    }
}
