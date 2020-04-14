using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
  public class AppDbContext : DbContext
  {
    [SuppressMessage("Compiler", "CS8618")]
    public DbSet<Company> Companies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      var company = new Company
      {
        Id = 1,
        Contacts =
        {
          new Contact
          {
            Name = "Shimmy",
            Phones =
            {
               new Phone { Number = "123456789" },
               new Phone { Number = "987654321" },
            }
          }
        }
      };

      modelBuilder
        .Entity<Company>()
        .HasData(company);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder
        .UseNpgsql("Host=localhost;Database=odata_server_client;Integrated Security=True;");
    }

  }
}
