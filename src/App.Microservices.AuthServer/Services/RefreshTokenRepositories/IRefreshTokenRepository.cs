using App.Microservices.AuthServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Microservices.AuthServer.Services.RefreshTokenRepositories;

public interface IRefreshTokenRepository
{
    Task<KnoxToken> GetByToken(string token);

    Task Create(KnoxToken refreshToken);

    Task Delete(Guid id);

    Task DeleteAll(Guid userId);
}
