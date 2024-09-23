using FluentValidation;
using ProductManagement.DTOs.Base;

namespace ProductManagement.Validators.Seller;

public class SellerValidator<T>: AbstractValidator<T>
    where T : BaseSellerDTO
{
    public SellerValidator()
    {   // SAME AS DB TABLE LENGTHs
        RuleFor(p=>p.Id).NotNull().WithMessage("ID cannot be null.");
        RuleFor(p=>p.Name).NotNull().WithMessage("Name cannot be null.").MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");
    }
}
