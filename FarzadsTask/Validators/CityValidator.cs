using FluentValidation;
using FarzadsTask.Dto;
namespace FarzadsTask.Validators
{
    public class CityValidator : AbstractValidator<CityDto>
    {
        public CityValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30); 
            
        }
    }
}
