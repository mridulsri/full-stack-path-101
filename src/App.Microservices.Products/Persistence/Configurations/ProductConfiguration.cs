using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using App.Microservices.Products.Models.Entites;
using App.Application.Interfaces;
using App.Application.Enums;

namespace App.Microservices.Products.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private readonly ICurrentUserService _currentUser;
    public ProductConfiguration(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Cost).HasConversion<decimal>();

        if (_currentUser.UserRole!=null && _currentUser.UserRole.Equals(UserRole.Standard))
            builder.HasQueryFilter(q => q.IsDeleted == false);
    }
}
