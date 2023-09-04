using Bogus;
using Employee.Integration.Application.UseCases.Employees.Commands.UpdateEmployee;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace Employee.Integration.UnitTest
{
    public class UpdateEmployeeCommandValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void UpdateEmployeeCommandValidator_IfPayroll_NumberIsNullOrEmpty_ShouldThrowValidationException(string Payroll_Number)
        {
            // Arrange
            var validator = new UpdateEmployeeCommandValidator();

            var UpdateEmployeeCommand = new UpdateEmployeeCommand()
            {
                Id = 1,
                Payroll_Number = Payroll_Number,
                Forenames = "Defalult Value",
                Surname = "Defalult Value",
                DateOfBirth = DateTime.Now,
                Telephone = "Defalult Value",
                Mobile = "Defalult Value",
                Address = "Defalult Value",
                Address_2 = "Defalult Value",
                Postcode = "Defalult Value",
                EMail_Home = "Defalult Value",
                StartDate = DateTime.Now
            };

            // Act
            var result = validator.TestValidate(UpdateEmployeeCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(item => item.Payroll_Number);
        }

        [Fact]
        public void UpdateEmployeeCommandValidator_IfForenameLengthIsMoreThan100_ShouldThrowValidationException()
        {
            // Arrange
            string Forename = new Faker().Random.String2(101);

            var validator = new UpdateEmployeeCommandValidator();

            var UpdateEmployeeCommand = new UpdateEmployeeCommand()
            {
                Id = 1,
                Payroll_Number = "Default Value",
                Forenames = Forename,
                Surname = "Defalult Value",
                DateOfBirth = DateTime.Now,
                Telephone = "Defalult Value",
                Mobile = "Defalult Value",
                Address = "Defalult Value",
                Address_2 = "Defalult Value",
                Postcode = "Defalult Value",
                EMail_Home = "Defalult Value",
                StartDate = DateTime.Now
            };

            // Act
            var result = validator.TestValidate(UpdateEmployeeCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(item => item.Forenames);
        }

        [Fact]
        public void UpdateEmployeeCommandValidator_IfSurnameLengthIsEqualTo100_ShouldSuccess()
        {
            // Arrange
            string surname = new Faker().Random.String2(100);
            var validator = new UpdateEmployeeCommandValidator();

            var UpdateEmployeeCommand = new UpdateEmployeeCommand()
            {
                Id = 1,
                Payroll_Number = "Default Value",
                Forenames = "Default Value",
                Surname = surname,
                DateOfBirth = DateTime.Now,
                Telephone = "Defalult Value",
                Mobile = "Defalult Value",
                Address = "Defalult Value",
                Address_2 = "Defalult Value",
                Postcode = "Defalult Value",
                EMail_Home = "Defalult Value",
                StartDate = DateTime.Now
            };

            // Act
            var result = validator.TestValidate(UpdateEmployeeCommand);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void UpdateEmployeeCommandValidator_IfDateOfBirthIsDefault_ShouldThrowValidationException()
        {
            // Arrange
            var validator = new UpdateEmployeeCommandValidator();

            var UpdateEmployeeCommand = new UpdateEmployeeCommand()
            {
                Id = 1,
                Payroll_Number = "Default Value",
                Forenames = "Default Value",
                Surname = "Default Value",
                DateOfBirth = default(DateTime),
                Telephone = "Defalult Value",
                Mobile = "Defalult Value",
                Address = "Defalult Value",
                Address_2 = "Defalult Value",
                Postcode = "Defalult Value",
                EMail_Home = "Defalult Value",
                StartDate = DateTime.Now
            };

            // Act
            var result = validator.TestValidate(UpdateEmployeeCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(item => item.DateOfBirth)
                .WithErrorMessage("Date Of Birth date is required.");
        }

        [Fact]
        public void UpdateEmployeeCommandValidator_IfTimeIsNotDefaultDateTime_ShouldPassValidation()
        {
            // Arrange
            var validator = new UpdateEmployeeCommandValidator();

            var UpdateEmployeeCommand = new UpdateEmployeeCommand()
            {
                Id = 1,
                Payroll_Number = "Default Value",
                Forenames = "Default Value",
                Surname = "Default Value",
                DateOfBirth = DateTime.Now,
                Telephone = "Defalult Value",
                Mobile = "Defalult Value",
                Address = "Defalult Value",
                Address_2 = "Defalult Value",
                Postcode = "Defalult Value",
                EMail_Home = "Defalult Value",
                StartDate = DateTime.Now
            };

            // Act
            var result = validator.TestValidate(UpdateEmployeeCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(item => item.DateOfBirth);
        }
    }
}
