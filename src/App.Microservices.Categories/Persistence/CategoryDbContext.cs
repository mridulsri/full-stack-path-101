using App.Application.Interfaces;
using App.Infrastructure.Persistence;
using App.Microservices.Categories.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace App.Microservices.Categories.Persistence;

public class CategoryDbContext:BaseDbContext
{
    public CategoryDbContext(
        DbContextOptions<CategoryDbContext> options,
        ICurrentUserService currentUserService,
        IDateTimeService dateTime
        ) :base(options, currentUserService, dateTime)
    {

    }

    public DbSet<Category> Categories { get; set; }
}
