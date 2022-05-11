using FluentValidation;
using webAPI.Models.DTO;
/* This class adds validation on the Models.DTO.AddRegionRequest model. 
 * Do not need to inject fluentValidation into API controller, the [apiController] tag is enough.
 * FluentValidator will automatically apply validation checks on data passed to HTTP action methods
 */
namespace webAPI.Validators
{
    public class AddRegionRequestValidator : AbstractValidator<AddRegionRequest>
    {
        public AddRegionRequestValidator()
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
