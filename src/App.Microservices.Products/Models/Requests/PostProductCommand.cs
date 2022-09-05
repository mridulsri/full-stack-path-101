using FluentValidation;
namespace App.Microservices.Products.Models.Requests;

public class PostProductCommand
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public string Category { get; set; }
}


public class PostProductCommandValidator:AbstractValidator<PostProductCommand>
{
    public PostProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Missing product name");
        RuleFor(x => x.Cost).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Missing product cost");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Missing product name");
    }
}

