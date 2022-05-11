using FluentValidation;
using webAPI.Models.DTO;

namespace webAPI.Validators
{
    public class UpdateRegionsRequestValidator : AbstractValidator<UpdateRegionRequest>
    {
        public UpdateRegionsRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Lat).InclusiveBetween(-90, 90);
            RuleFor(x => x.Long).InclusiveBetween(-180, 180);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
