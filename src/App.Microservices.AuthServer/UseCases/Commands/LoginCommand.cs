using FluentValidation;
using MediatR;
using App.Microservices.AuthServer.Models.Entites;
using App.Microservices.AuthServer.Persistence;
using App.Microservices.AuthServer.Helpers;

namespace App.Microservices.AuthServer.UseCases.Commands;

public class LoginCommand //: IRequest<ApplicationUser>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(login => login.UserName).NotEmpty().WithMessage("Missing username.");
        RuleFor(login => login.Password).NotEmpty().WithMessage("Missing password");
    }
}

/*
public class LoginCommandHandler : IRequestHandler<LoginCommand, ApplicationUser>
{
    private readonly AuthDbContext _dbContext;
    public LoginCommandHandler(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    async Task<ApplicationUser> IRequestHandler<LoginCommand, ApplicationUser>.Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = _dbContext.Users.Any(x => x.Username.Equals(request.UserName.Trim()));

        if (isUserExist)
            return null;

        var entiry = new ApplicationUser
        {
            Username = request.UserName,
            Password = request.Password,
        };

        await _dbContext.SaveChangesAsync();

        return entiry.WithoutPassword();
    }
}
*/