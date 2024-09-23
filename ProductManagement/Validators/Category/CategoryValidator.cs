using FluentValidation;
using ProductManagement.DTOs.Base;

namespace ProductManagement.Validators.Category;

public class CategoryValidator<T>: AbstractValidator<T>
    where T : BaseCategoryDTO
{
    public CategoryValidator()
    {   // SAME AS DB TABLE LENGTHs
        RuleFor(p=>p.Name).NotNull().WithMessage("Name cannot be null.").MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");
        RuleFor(p=>p.Description).NotNull().WithMessage("Description cannot be null.");
    }
}
