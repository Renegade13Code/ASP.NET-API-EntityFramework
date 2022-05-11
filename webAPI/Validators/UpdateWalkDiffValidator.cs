using FluentValidation;
using webAPI.Models.DTO;

namespace webAPI.Validators
{
    public class UpdateWalkDiffValidator : AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDiffValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
