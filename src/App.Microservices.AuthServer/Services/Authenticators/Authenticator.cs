using App.Microservices.AuthServer.Entities;
using App.Microservices.AuthServer.Models.Domain;
using App.Microservices.AuthServer.Services.TokenGenerators;
using App.Microservices.AuthServer.Services.RefreshTokenRepositories;
using App.Microservices.AuthServer.Models.Entites;

namespace App.Microservices.AuthServer.Services.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public Authenticator(AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator, 
            IRefreshTokenRepository refreshTokenRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticatedUser> Authenticate(ApplicationUser user, bool isImpersonated = false, string currentUserId = null)
        {
            AccessToken accessToken = _accessTokenGenerator.GenerateToken(user, isImpersonated, currentUserId);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            KnoxToken refreshTokenDTO = new KnoxToken()
            {
                Token = refreshToken,
                UserId = user.UserId
            };
            await _refreshTokenRepository.Create(refreshTokenDTO);

            return new AuthenticatedUser()
            {
                AccessToken = accessToken.Value,
                AccessTokenExpirationTime = accessToken.ExpirationTime,
                RefreshToken = refreshToken
            };
        }
    }
}
