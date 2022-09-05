using App.Application.Enums;
using App.Application.Interfaces;
using App.Microservices.AuthServer.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Microservices.AuthServer.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly IPasswordHasher _hasher;
        public ApplicationUserConfiguration(IPasswordHasher hasher)
        {
            _hasher = hasher;
        }
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            var accountTypeEnumConverter = new ValueConverter<UserRole, string>(
            ep => ep.ToString(),
            ep => (UserRole)Enum.Parse(typeof(UserRole), ep)
            );

            builder.Property(p=>p.Role).HasConversion(accountTypeEnumConverter);

            // seed deafult data
            builder.HasData(
                new ApplicationUser
                {
                    Id = 1,
                    Username = "demo@demo.me",
                    Name = "demo",
                    Email = "demo@demo.me",
                    Phone = "9350272167",
                    Password = _hasher.Hash("Password@123"),
                    DOB = DateTime.UtcNow,
                    Gender = "Male",
                    Role = UserRole.Standard
                }

                ); 
        }
    }
}
