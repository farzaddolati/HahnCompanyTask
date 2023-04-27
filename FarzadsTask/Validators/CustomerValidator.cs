using FluentValidation;
using FarzadsTask.Dto;

namespace FarzadsTask.Validators
{
    public class CustomerValidator: AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
