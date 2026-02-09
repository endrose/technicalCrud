using Microsoft.EntityFrameworkCore;
using technicalTestCrud.Models;

namespace technicalTestCrud.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //## SET PRODUCT DB
    public DbSet<Product> Products { get; set; }
  }
}