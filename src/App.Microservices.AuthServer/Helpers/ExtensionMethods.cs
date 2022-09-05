using App.Microservices.AuthServer.Models.Entites;

namespace App.Microservices.AuthServer.Helpers;

public static class ExtensionMethods
{
    public static IEnumerable<ApplicationUser> WithoutPasswords(this IEnumerable<ApplicationUser> users)
    {
        return users.Select(x => x.WithoutPassword());
    }

    public static ApplicationUser WithoutPassword(this ApplicationUser user)
    {
        user.Password = null;
        return user;
    }
}
