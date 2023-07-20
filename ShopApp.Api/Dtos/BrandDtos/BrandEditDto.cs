using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Api.Dtos.BrandDtos
{
    public class BrandEditDto
    {

        public string Name { get; set; }
    }


    public class BrandEditDtoValidator:AbstractValidator<BrandEditDto>
    {
        public BrandEditDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(35);
        }
    }
}
