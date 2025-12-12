using FluentValidation;
using Plais.DTOs.ExecutiveMember;

namespace Plais.Validators
{
    public class SaveExecutiveMemberDtoValidator : AbstractValidator<SaveExecutiveMemberDto>
    {
        public SaveExecutiveMemberDtoValidator()
        {

            RuleFor(em => em.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Insert a valid email");

        }
    }
}
