using FluentValidation;
using webAPI.Models.DTO;

namespace webAPI.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
