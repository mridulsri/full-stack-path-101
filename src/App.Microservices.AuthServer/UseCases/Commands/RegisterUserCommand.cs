using FluentValidation;

namespace App.Microservices.AuthServer.Commands
{
    public class RegisterUserCommand
    {
        public string Email { get; set; }

        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }


    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(v => v.Email).NotEmpty().EmailAddress().WithMessage("Please enter valid email.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Please enter name.");
            RuleFor(v => v.DOB).NotEmpty().WithMessage("Please enter DOB.");
            RuleFor(v => v.Gender).NotEmpty().WithMessage("Please enter gender.");
            RuleFor(v => v.Password).NotEmpty().MinimumLength(6).WithMessage("Please enter valid password.");
            RuleFor(v => v.ConfirmPassword).NotEmpty()
                .Must((model, field) => field == model.Password)
                .WithMessage("Password does not match confirm password.");
        }
    }

    
}
