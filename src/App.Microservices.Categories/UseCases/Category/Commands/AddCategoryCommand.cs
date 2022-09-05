using FluentValidation;

namespace App.Microservices.Categories.UseCases.Category.Commands;

public class AddCategoryCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class AddCategoryCommandValidator: AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Missing category name");
    }
}