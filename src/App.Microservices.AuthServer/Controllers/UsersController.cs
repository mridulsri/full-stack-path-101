using App.Application.Interfaces;
using App.Application.Models;
using App.Microservices.AuthServer.Models.Entites;
using App.Microservices.AuthServer.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.Microservices.AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthDbContext _dbContext;
        private readonly IPasswordHasher _hasher;
        public UsersController(
        IHttpContextAccessor httpContextAccessor,
        AuthDbContext dbContext,
        IPasswordHasher hasher
        )
        {  _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _hasher = hasher;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _dbContext.Users.ToListAsync();
            return Ok(new SuccessResponse<List<ApplicationUser>>
            {
                Data = users
            });
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
