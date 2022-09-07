using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.Microservices.AuthServer.Models.Entites;
using App.Microservices.AuthServer.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using App.Microservices.Framework.Jwt;
using Microsoft.AspNetCore.Authorization;
using App.Microservices.AuthServer.Services.Authenticators;
using App.Microservices.AuthServer.Services.RefreshTokenRepositories;
using App.Microservices.AuthServer.Configs;
using App.Microservices.AuthServer.Services.TokenValidators;
using App.Microservices.AuthServer.Models.Domain;
using MediatR;
using App.Microservices.Framework.Controllers;
using App.Microservices.AuthServer.Persistence;
using App.Microservices.AuthServer.Helpers;
using App.Application.Interfaces;
using App.Application.Models;
using App.Microservices.AuthServer.UseCases.Commands;

namespace App.Microservices.AuthServer.Controllers;

[Route("api/[controller]")]
// [ApiController]
public class AuthController : ControllerBase
{
    private readonly Authenticator _authenticator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly RefreshTokenValidator _refreshTokenValidator;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly AuthDbContext _dbContext;
    private readonly IPasswordHasher _hasher;
    private readonly ILogger _logger;

    public AuthController(Authenticator authenticator,
        RefreshTokenValidator refreshTokenValidator,
        IRefreshTokenRepository refreshTokenRepository,
        IHttpContextAccessor httpContextAccessor,
        AuthDbContext dbContext,
        IPasswordHasher hasher,
        ILogger<AuthController> logger
        )
    {
        _authenticator = authenticator;
        _refreshTokenRepository = refreshTokenRepository;
        _refreshTokenValidator = refreshTokenValidator;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
        _hasher = hasher;
        _logger = logger;
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        // Tried CQRS pattern using Mediator
        //  var user = await Mediator.Send(loginCommand);
        _logger.LogInformation("logging the data");
        var user = _dbContext.Users.FirstOrDefault(x => x.Username.Equals(loginCommand.UserName.Trim()));
        if (user == null)
        {
            return Unauthorized();
        }
        AuthenticatedUser response = await _authenticator.Authenticate(user);
        return Ok(response);
    }

    [Authorize]
    [HttpDelete("logout")]
    public async Task<IActionResult> Logout(string id)
    {
        if (!Guid.TryParse(id, out Guid userId))
        {
            return Unauthorized();
        }
        await _refreshTokenRepository.DeleteAll(userId);
        return NoContent();
    }



    [HttpPost("token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromRoute] string referhToken)
    {

        //if (rt == null)
        //{
        //    return Unauthorized();
        //}
        //if (!_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
        //{
        //    return Unauthorized();
        //}

        var bearer = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var agent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

        //if(rt.AgentDetails == agent)
        //{

        //}

        return Ok(referhToken);
    }
}
