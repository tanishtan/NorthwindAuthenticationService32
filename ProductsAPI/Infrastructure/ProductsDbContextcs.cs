using Microsoft.EntityFrameworkCore;
using NorthwindModelClassLibrary;
using System.Collections.Generic;

namespace ProductsAPI.Infrastructure
{
    public class ProductsDbContextcs : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDbContextcs(DbContextOptions<ProductsDbContextcs> options) : base(options) { }
        
    }
}
