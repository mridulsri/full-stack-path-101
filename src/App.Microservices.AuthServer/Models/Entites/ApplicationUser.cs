using App.Application.Enums;
using App.Application.Persistence;

namespace App.Microservices.AuthServer.Models.Entites;

public class ApplicationUser: BaseEntity
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string Username { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTime DOB { get; set; }
    public string Gender { get; set; }
    public UserRole Role { get; set; }

 }
