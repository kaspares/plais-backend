using FluentValidation;
using Plais.DTOs.Cadence;
using Plais.DTOs.CadenceMembership;

namespace Plais.Validators
{
    public class SaveCadenceMembershipDtoValidator : AbstractValidator<SaveCadenceMembershipDto>
    {
        public SaveCadenceMembershipDtoValidator()
        {
            RuleFor(ms => ms.CadenceId)
                .GreaterThan(0);

            RuleFor(em => em.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .Length(3, 100).WithMessage("Insert at least 3 characters.");


            RuleFor(em => em.Department)
                .NotEmpty().WithMessage("Department is required")
                .Length(3, 100).WithMessage("Insert at least 3 characters.");


            RuleFor(ms => ms.Role)
                .NotEmpty().WithMessage("Role is required");

            RuleFor(ms => ms.Position)
                .GreaterThan(0).WithMessage("Position must be greater than 0");
        }
    }
}
