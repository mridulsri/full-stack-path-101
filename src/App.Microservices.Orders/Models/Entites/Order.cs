using App.Infrastructure.Persistence;
using App.Application.Entites;

namespace App.Microservices.Orders.Models.Entites;

public class Order: AuditableEntity
{
    public Guid OrderId { get; set; } = Guid.NewGuid();
    public string UserId { get; set; }
    public int ProductId { get; set; }
}
