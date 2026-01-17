using FluentValidation.TestHelper;
using Plais.DTOs.CadenceMembership;
using Plais.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Plais.Validators.Tests
{
    public class SaveCadenceMembershipDtoValidatorTests
    {
        private readonly SaveCadenceMembershipDtoValidator _validator = new();

        private static SaveCadenceMembershipDto CreateValidDto() => new SaveCadenceMembershipDto
        {
            CadenceId = 1,
            FullName = "Jan Kowalski",
            Department = "Wydzial Zarzadzania ",
            Role = "President",
            Position = 1,
            PhotoFileName = null
        };

        [Fact()]
        public void Validator_ForValidDto_ShouldNotHaveValidationErrors()
        {
            var dto = CreateValidDto();

            var result = _validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0, "ja", "wy", "", -1)]
        public void Validator_ForInvalidDto_ShouldHaveValidationErrors(int cadenceId, string fullName, string department, string role, int position)
        {
            var dto = CreateValidDto();
            dto.CadenceId = cadenceId;
            dto.FullName = fullName;
            dto.Department = department;
            dto.Role = role;
            dto.Position = position;

            var result = _validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(d => d.CadenceId);
            result.ShouldHaveValidationErrorFor(d => d.FullName);
            result.ShouldHaveValidationErrorFor(d => d.Department);
            result.ShouldHaveValidationErrorFor(d => d.Role);
            result.ShouldHaveValidationErrorFor(d => d.Position);
        }
    }
}