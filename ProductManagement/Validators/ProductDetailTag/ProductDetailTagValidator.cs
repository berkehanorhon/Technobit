using FluentValidation;
using ProductManagement.DTOs.Base;

namespace ProductManagement.Validators.ProductDetailTag;

public class ProductDetailTagValidator<T>: AbstractValidator<T>
    where T : BaseProductDetailTagDTO
{
    public ProductDetailTagValidator()
    {   // SAME AS DB TABLE LENGTHs
        RuleFor(p=>p.Productid).NotNull().WithMessage("Product ID cannot be null.");
        RuleFor(p=>p.Title).NotNull().WithMessage("Name cannot be null.").MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");
        RuleFor(p=>p.Description).NotNull().WithMessage("Description cannot be null.").MaximumLength(50).WithMessage("Description cannot be longer than 50 characters.");
    }
}
