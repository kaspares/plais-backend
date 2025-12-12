using FluentValidation;
using Plais.DTOs.Bulletin;

namespace Plais.Validators
{
    public class SaveBulletinDtoValidator : AbstractValidator<SaveBulletinDto>
    {
        public SaveBulletinDtoValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required")
                .Length(3, 100);

            RuleFor(b => b.Content)
                .NotEmpty().WithMessage("Content is required");
                
        }
    }
}
