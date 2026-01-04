using Eshop.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Persistence
{
    public class EshopDbContext : DbContext
    {
        public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; private set; }
        public IQueryable<Category> CategoriesViews => Categories.AsNoTracking();
        public DbSet<Product> Products { get; private set; }
        public IQueryable<Product> ProductsViews => Products.AsNoTracking();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
