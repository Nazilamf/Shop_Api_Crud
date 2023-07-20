using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Api.Dtos.BrandDtos
{
    public class BrandCreateDto
    {
        public string Name { get; set; }
    }

    public class BrandCreateDtoValidator:AbstractValidator<BrandCreateDto>

    {
        public BrandCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bos ola bilmez").MinimumLength(3).MaximumLength(35);
        }
    }

}
