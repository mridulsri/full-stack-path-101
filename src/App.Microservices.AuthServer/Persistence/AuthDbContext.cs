using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using App.Microservices.AuthServer.Entities;
using App.Application.Interfaces;
using App.Microservices.AuthServer.Models.Entites;
using System.Reflection;
using App.Microservices.AuthServer.Persistence.Configurations;

namespace App.Microservices.AuthServer.Persistence;

public class AuthDbContext: BaseDbContext
{
    private readonly IPasswordHasher _hasher;
    public AuthDbContext(DbContextOptions<AuthDbContext> options,
        ICurrentUserService currentUserService,
        IDateTimeService dateTime,
        IPasswordHasher hasher
        ) : base(options, currentUserService, dateTime)
    {
        _hasher = hasher;
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<KnoxToken> KnoxTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration(_hasher));
        base.OnModelCreating(modelBuilder);
    }
}
