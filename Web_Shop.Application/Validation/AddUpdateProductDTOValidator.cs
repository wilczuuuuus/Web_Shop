using FluentValidation;
using Web_Shop.Application.DTOs;

namespace Web_Shop.Application.Validation
{
    public class AddUpdateProductDTOValidator : AbstractValidator<AddUpdateProductDTO>
    {
        public AddUpdateProductDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(255).WithMessage("Product name cannot exceed 255 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required")
                .MaximumLength(1000).WithMessage("Product description cannot exceed 1000 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("SKU is required")
                .MaximumLength(100).WithMessage("SKU cannot exceed 100 characters");
        }
    }
} 