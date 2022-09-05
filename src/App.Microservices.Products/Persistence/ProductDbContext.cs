using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using App.Microservices.Products.Persistence.Configurations;
using App.Application.Interfaces;
using App.Microservices.Products.Models.Entites;

namespace App.Microservices.Products.Persistence
{
    public class ProductDbContext : BaseDbContext
    {
        private readonly ICurrentUserService _currentUser;
        public ProductDbContext(DbContextOptions<ProductDbContext> options,
                ICurrentUserService currentUserService,
                IDateTimeService dateTime
                ) : base(options, currentUserService, dateTime)
        {
            _currentUser = currentUserService;
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration(_currentUser));

            BuildTables(modelBuilder);
            BuildIndex(modelBuilder);
            BuildCompositeKey(modelBuilder);
            BuildFilters(modelBuilder);
            BuildEnumConversions(modelBuilder);
        }

        private void BuildTables(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Category>().ToTable(nameof(Categories));
        }

        private void BuildIndex(ModelBuilder modelBuilder)
        {

        }
        private void BuildCompositeKey(ModelBuilder modelBuilder)
        {

        }

        private void BuildFilters(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Category>().HasQueryFilter(f => !f.IsDeleted);
        }

        private void BuildEnumConversions(ModelBuilder modelBuilder)
        {
            //var categoryTypeEnumConverter = new ValueConverter<CategoryType, string>(
            //    ep => ep.ToString(),
            //    ep => (CategoryType)Enum.Parse(typeof(CategoryType), ep)
            //    );

            //modelBuilder.Entity<Category>().Property(c => c.CategoryType).HasConversion(categoryTypeEnumConverter);
        }
    }
}
