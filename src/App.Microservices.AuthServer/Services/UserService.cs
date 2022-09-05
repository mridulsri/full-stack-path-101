using App.Microservices.AuthServer.Helpers;
using App.Microservices.AuthServer.Models.Entites;

namespace App.Microservices.AuthServer.Services;

public interface IUserService
{
    Task<ApplicationUser> Authenticate(string username, string password);
    Task<IEnumerable<ApplicationUser>> GetAll();
}

public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<ApplicationUser> _users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = 1, Name = "Test", Username = "test@test.me", Password = "test" }
        };

    public async Task<ApplicationUser> Authenticate(string username, string password)
    {
        var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

        // return null if user not found
        if (user == null)
            return null;

        // authentication successful so return user details without password
        return user.WithoutPassword();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAll()
    {
        return await Task.Run(() => _users.WithoutPasswords());
    }
}
