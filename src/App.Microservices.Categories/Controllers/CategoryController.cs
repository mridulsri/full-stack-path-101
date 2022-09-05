using App.Application.Models;
using App.Microservices.Categories.Models.Entites;
using App.Microservices.Categories.Persistence;
using App.Microservices.Categories.UseCases.Category.Commands;
using App.Microservices.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.Microservices.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiControllerBase
    {
        private readonly CategoryDbContext _dbContext;
        public CategoryController(CategoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var categories = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{query}")]
        public async Task<IActionResult> Search(string query)
        {
            var categoryQuery = _dbContext.Categories.AsQueryable();
            if(string.IsNullOrEmpty(query))
            {
                categoryQuery.Where(x => x.Name.Contains(query.Trim()));
            }
            var categories = await categoryQuery.ToListAsync();
            var response = new SuccessResponse<List<Category>>
            {
                Data = categories
            };
            return Ok(response);
            
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCategoryCommand command)
        {
            var category = _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == command.Name);
            if (category is null)
            {
                return BadRequest($"Category with name [{command.Name}] already exist in system");
            }

            var newCategory = new Models.Entites.Category
            {
                Name = command.Name,
                Descrition = command.Description
                
            };
           
            _dbContext.Categories.Add(newCategory);

            var result = await _dbContext.SaveChangesAsync();

            return Ok(newCategory);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
