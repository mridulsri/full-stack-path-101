using App.Infrastructure.Persistence;
using App.Application.Entites;

namespace App.Microservices.Products.Models.Entites;

public class Product: AuditableEntity
{
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public string Category { get; set; }
}





