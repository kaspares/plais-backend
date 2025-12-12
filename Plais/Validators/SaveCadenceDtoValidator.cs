using FluentValidation;
using Plais.DTOs.Cadence;

namespace Plais.Validators
{
    public class SaveCadenceDtoValidator : AbstractValidator<SaveCadenceDto>
    {
        public SaveCadenceDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Cadence name is required")
                .Length(3, 100).WithMessage("Insert at least 3  characters.");

        }
    }
}
