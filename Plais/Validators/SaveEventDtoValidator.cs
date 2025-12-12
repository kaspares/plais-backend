using FluentValidation;
using Plais.DTOs.Cadence;
using Plais.DTOs.Event;

namespace Plais.Validators
{
    public class SaveEventDtoValidator : AbstractValidator<SaveEventDto>
    {
        public SaveEventDtoValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Event name is required")
                .Length(3, 100).WithMessage("Insert at least 3  characters.");

            RuleFor(e => e.LinkUrl)
                .NotEmpty().WithMessage("Event link is required")
                .Length(3, 200);
        }
    }
}
