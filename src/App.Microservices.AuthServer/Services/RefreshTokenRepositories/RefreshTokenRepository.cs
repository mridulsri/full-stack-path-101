using Microsoft.EntityFrameworkCore;
using App.Microservices.AuthServer.Entities;
using App.Microservices.AuthServer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Microservices.AuthServer.Services.RefreshTokenRepositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthDbContext _context;

        public RefreshTokenRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task Create(KnoxToken refreshToken)
        {
            _context.KnoxTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            KnoxToken refreshToken = await _context.KnoxTokens.FindAsync(id);
            if(refreshToken != null)
            {
                _context.KnoxTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAll(Guid userId)
        {
            IEnumerable<KnoxToken> refreshTokens = await _context.KnoxTokens
                .Where(t => t.UserId == userId)
                .ToListAsync();

            _context.KnoxTokens.RemoveRange(refreshTokens);
            await _context.SaveChangesAsync();
        }

        public async Task<KnoxToken> GetByToken(string token)
        {
            return await _context.KnoxTokens.FirstOrDefaultAsync(t => t.Token == token);
        }
    }
}
