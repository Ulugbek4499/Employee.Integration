using Bogus;
using Employee.Integration.Application.UseCases.Employees.Commands.CreateEmployee;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace Employee.Integration.UnitTest
{
    public class CreateEmployeeCommandValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void CreateEmployeeCommandValidator_IfPayroll_NumberIsNullOrEmpty_ShouldThrowValidationException(string Payroll_Number)
        {
            // Arrange
            var validator = new CreateEmployeeCommandValidator();

            var createEmployeeCommand = new CreateEmployeeCommand()
            {
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
            var result = validator.TestValidate(createEmployeeCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(item => item.Payroll_Number);
        }

        [Fact]
        public void CreateEmployeeCommandValidator_IfForenameLengthIsMoreThan100_ShouldThrowValidationException()
        {
            // Arrange
            string Forename = new Faker().Random.String2(101);

            var validator = new CreateEmployeeCommandValidator();

            var createEmployeeCommand = new CreateEmployeeCommand()
            {
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
            var result = validator.TestValidate(createEmployeeCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(item => item.Forenames);
        }

        [Fact]
        public void CreateEmployeeCommandValidator_IfSurnameLengthIsEqualTo100_ShouldSuccess()
        {
            // Arrange
            string surname = new Faker().Random.String2(100);
            var validator = new CreateEmployeeCommandValidator();

            var createEmployeeCommand = new CreateEmployeeCommand()
            {
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
            var result = validator.TestValidate(createEmployeeCommand);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void CreateEmployeeCommandValidator_IfDateOfBirthIsDefault_ShouldThrowValidationException()
        {
            // Arrange
            var validator = new CreateEmployeeCommandValidator();

            var createEmployeeCommand = new CreateEmployeeCommand()
            {
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
            var result = validator.TestValidate(createEmployeeCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(item => item.DateOfBirth)
                .WithErrorMessage("Date Of Birth date is required.");
        }

        [Fact]
        public void CreateEmployeeCommandValidator_IfTimeIsNotDefaultDateTime_ShouldPassValidation()
        {
            // Arrange
            var validator = new CreateEmployeeCommandValidator();

            var createEmployeeCommand = new CreateEmployeeCommand()
            {
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
            var result = validator.TestValidate(createEmployeeCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(item => item.DateOfBirth);
        }
    }
}
