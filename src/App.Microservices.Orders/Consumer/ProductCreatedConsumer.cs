using App.Microservices.Orders.Models.Entites;
using App.Microservices.Orders.Persistence;
using Application.Models.RabbitMqModel;
using MassTransit;

namespace App.Microservices.Orders.Consumer
{
    public class ProductCreatedConsumer : IConsumer<ProductCreated>
    {
        private readonly OrderDbContext _dbContext;

        public ProductCreatedConsumer(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            var newProduct = new Product
            {
                Id = context.Message.Id,
                Name = context.Message.Name
            };
            _dbContext.Products.Add(newProduct);
            await _dbContext.SaveChangesAsync();
        }
    }
}
