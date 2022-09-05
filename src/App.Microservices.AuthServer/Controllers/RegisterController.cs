using App.Application.Interfaces;
using App.Application.Models;
using App.Microservices.AuthServer.Commands;
using App.Microservices.AuthServer.Models.Entites;
using App.Microservices.AuthServer.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Microservices.AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AuthDbContext _dbContext;
        private readonly IPasswordHasher _hasher;

        public RegisterController(AuthDbContext dbContext,IPasswordHasher hasher)
        {
            _dbContext = dbContext;
            _hasher = hasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            var isUserExist = _dbContext.Users.Any(x => x.Username.Equals(registerUserCommand.Email));

            if (isUserExist)
            {
                return BadRequest(new ErrorResponse($"User already exist having email {registerUserCommand.Email}"));
            }
            var entity = new ApplicationUser()
            {
                Username = registerUserCommand.Email,
                Name= registerUserCommand.Name,
                DOB=registerUserCommand.DOB,
                Gender = registerUserCommand.Gender,                
                Password = _hasher.Hash(registerUserCommand.Password),
            };

            await _dbContext.Users.AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return BadRequest(
                new ErrorResponse("Something went wrong"));
            }

            return Ok("User added sucessfully");
        }

        
         
    }
}
