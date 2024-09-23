using FluentValidation;
using ProductManagement.DTOs.Base;

namespace ProductManagement.Validators.SellerProduct;

public class SellerProductValidator<T>: AbstractValidator<T>
    where T : BaseSellerProductDTO
{
    public SellerProductValidator()
    {   // SAME AS DB TABLE LENGTHs
        RuleFor(p=>p.Productid).NotNull().WithMessage("Product ID cannot be null.");
        RuleFor(p=>p.Sellerid).NotNull().WithMessage("Seller ID cannot be null.");
        RuleFor(p=>p.Price).NotNull().WithMessage("Price cannot be null.").GreaterThan(0).WithMessage("Price must be greater than or equal to 1.");
        RuleFor(p=>p.Stockquantity).NotNull().WithMessage("Stock quantity cannot be null.").GreaterThanOrEqualTo(1).WithMessage("Stock quantity must be greater than or equal to 1.");
    }
}
