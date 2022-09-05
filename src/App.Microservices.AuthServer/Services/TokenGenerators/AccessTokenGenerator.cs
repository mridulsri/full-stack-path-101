using Microsoft.IdentityModel.Tokens;
using App.Microservices.AuthServer.Models.Entites;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using App.Microservices.Framework.ConfigOptions;

namespace App.Microservices.AuthServer.Services.TokenGenerators;

public class AccessTokenGenerator
{
    private readonly AuthenticationOption _configuration;
    private readonly TokenGenerator _tokenGenerator;

    public AccessTokenGenerator(AuthenticationOption configuration, TokenGenerator tokenGenerator)
    {
        _configuration = configuration;
        _tokenGenerator = tokenGenerator;
    }

    public AccessToken GenerateToken(ApplicationUser user, bool isImpersonated = false, string currentUserId = null)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };

        /*
        if(isImpersonated)
        {
            claims.Add(new Claim("OriginalUserId", currentUserId));
            claims.Add(new Claim("IsImpersonating", "true"));
        }
        */

        DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes);
        string value = _tokenGenerator.GenerateToken(
            _configuration.AccessTokenSecret,
            _configuration.Issuer,
            _configuration.Audience,
            expirationTime,
            claims);

        return new AccessToken()
        {
            Value = value,
            ExpirationTime = expirationTime
        };
    }
}
