using FluentValidation;
using ProductManagement.Models;


public class ProductValidator: AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p=>p.Name).NotNull().MaximumLength(1);
        RuleFor(p=>p.Description).NotNull().MaximumLength(1);
    }
}
