using Eshop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Persistence
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title);
            builder.Property(x => x.Description);
            builder.Property(x => x.Price);

            builder.HasOne(x => x.Category)
                   .WithMany()
                   .HasForeignKey("CategoryId")
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired(false);
        }
    }
}
