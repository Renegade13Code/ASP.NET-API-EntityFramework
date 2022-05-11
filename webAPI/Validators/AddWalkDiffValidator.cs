using FluentValidation;
using webAPI.Models.DTO;

namespace webAPI.Validators
{
    public class AddWalkDiffValidator : AbstractValidator<AddWalkDifficultyRequest>
    {
        public AddWalkDiffValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
