using App.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Microservices.Products.Models.Requests;
using App.Microservices.Products.Persistence;
using App.Microservices.Products.Models.Entites;
using Microsoft.AspNetCore.Authorization;
using MassTransit;
using Application.Models.RabbitMqModel;

namespace App.Microservices.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        public readonly ProductDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public ProductsController(ProductDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var products = await _dbContext.Products.ToListAsync();
            if(!products.Any())
            {
                return NotFound(new ErrorResponse("Product not found"));
            }
            return Ok(new SuccessResponse<List<Product>>
            {
                Data = new List<Product>(products)
            });
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] PostProductCommand command)
        {

            var newEntity = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Cost = command.Cost,
                Category="digital"
            };

            await _dbContext.Products.AddAsync(newEntity);
            var id = await  _dbContext.SaveChangesAsync();

            try
            {
                await _publishEndpoint.Publish<ProductCreated>(new ProductCreated
                {
                    Id = newEntity.Id,
                    Name = newEntity.Name
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (id <=0)
                return BadRequest(new ErrorResponse("Something went wrong"));
            
            return Ok(new SuccessResponse<Product>
            {
                Data = newEntity
            });
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid productId))
            {
                return NotFound(new ErrorResponse("Invalid id."));
            }

            var entity = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (entity is null)
                return NotFound(new ErrorResponse("Product not found"));

            entity.IsDeleted = true;

            var result = await _dbContext.SaveChangesAsync();

            if (result <= 0)
                return BadRequest(new ErrorResponse("Something went wrong"));

            return NoContent();
        }
    }
}
