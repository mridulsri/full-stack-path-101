using App.Microservices.Categories.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Microservices.Categories.Persistence.Configurations;

public class CategoryCofiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Descrition).HasDefaultValue(null);

       //  builder.HasQueryFilter(f=>f)
    }
}
