using FluentValidation;

namespace LogisticsApp.Application.Aggregates.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.RefCode)
            .NotEmpty().WithMessage("Reference code is required.")
            .MaximumLength(50).WithMessage("Reference code cannot exceed 50 characters.");

        RuleFor(x => x.Season)
            .NotEmpty().WithMessage("Season is required.")
            .MaximumLength(100).WithMessage("Season cannot exceed 100 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.Categories)
            .NotNull().WithMessage("Categories cannot be null.")
            .Must(cats => cats == null || cats.Count <= 10).WithMessage("A product can have a maximum of 10 categories.")
            .ForEach(cat => cat.NotEmpty().WithMessage("Category names cannot be empty.")
                               .MaximumLength(100).WithMessage("Category names cannot exceed 100 characters."));

        RuleFor(x => x.Colors)
            .NotNull().WithMessage("Colors cannot be null.")
            .Must(colors => colors == null || colors.Count <= 10).WithMessage("A product can have a maximum of 10 colors.")
            .ForEach(color => color.NotEmpty().WithMessage("Color names cannot be empty.")
                                  .MaximumLength(50).WithMessage("Color names cannot exceed 50 characters."));

        RuleFor(x => x.Sizes)
            .NotNull().WithMessage("Sizes cannot be null.")
            .Must(sizes => sizes == null || sizes.Count <= 10).WithMessage("A product can have a maximum of 10 sizes.")
            .ForEach(size => size.NotEmpty().WithMessage("Size names cannot be empty.")
                                 .MaximumLength(20).WithMessage("Size names cannot exceed 20 characters."));
    }
}