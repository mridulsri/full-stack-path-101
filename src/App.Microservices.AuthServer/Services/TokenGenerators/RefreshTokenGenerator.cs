using App.Microservices.Framework.ConfigOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Microservices.AuthServer.Services.TokenGenerators;

public class RefreshTokenGenerator
{
    private readonly AuthenticationOption _option;
    private readonly TokenGenerator _tokenGenerator;

    public RefreshTokenGenerator(AuthenticationOption option, TokenGenerator tokenGenerator)
    {
        _option = option;
        _tokenGenerator = tokenGenerator;
    }

    public string GenerateToken()
    {
        DateTime expirationTime = DateTime.UtcNow.AddMinutes(_option.RefreshTokenExpirationMinutes);

        return _tokenGenerator.GenerateToken(
            _option.RefreshTokenSecret,
            _option.Issuer,
            _option.Audience,
            expirationTime);
    }
}
