using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using App.Microservices.Orders.Models.Entites;
using App.Application.Interfaces;

namespace App.Microservices.Orders.Persistence
{
    public class OrderDbContext:BaseDbContext
    {
        public OrderDbContext(
        DbContextOptions<OrderDbContext> options,
        ICurrentUserService currentUserService,
        IDateTimeService dateTime
        ) : base(options, currentUserService, dateTime)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
