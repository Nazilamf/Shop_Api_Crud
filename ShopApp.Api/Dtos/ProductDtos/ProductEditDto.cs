using FluentValidation;

namespace ShopApp.Api.Dtos.ProductDtos
{
    public class ProductEditDto
    {


        public int BrandId { get; set; }
        public string Name { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }


    }

        public class ProductEditDtoValidator: AbstractValidator<ProductEditDto> 
    {
            public ProductEditDtoValidator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.SalePrice).NotEmpty().GreaterThanOrEqualTo(x => x.CostPrice);
                RuleFor(x => x.CostPrice).NotEmpty().GreaterThanOrEqualTo(0);
                RuleFor(x => x.BrandId).GreaterThanOrEqualTo(1);
            }
        }
}
