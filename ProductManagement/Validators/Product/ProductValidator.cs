using FluentValidation;
using ProductManagement.DTOs.Base;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;

namespace ProductManagement.Validators;
public class ProductValidator<T>: AbstractValidator<T>
where T : BaseProductDTO
{
    public ProductValidator()
    {   // SAME AS DB TABLE LENGTHs
        RuleFor(p=>p.Categoryid).NotNull().WithMessage("Category ID cannot be null.");
        RuleFor(p=>p.Name).NotNull().WithMessage("Name cannot be null.").MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");
        RuleFor(p=>p.Description).NotNull().WithMessage("Description cannot be null.");
    }
}
