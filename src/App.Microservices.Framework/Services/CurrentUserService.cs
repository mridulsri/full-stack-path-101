
using App.Application.Enums;
using App.Application.Interfaces;
using System.Security.Claims;

namespace App.Microservices.Framework.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ;
    public UserRole UserRole => _httpContextAccessor.HttpContext?.User == null? UserRole.Standard : (UserRole)Enum.Parse(typeof(UserRole),_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role));
}
