using FluentValidation;

namespace Employee.Integration.Application.UseCases.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(d => d.Payroll_Number)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Payroll_Number is required");

            RuleFor(d => d.Forenames)
               .NotEmpty()
               .MaximumLength(100)
               .WithMessage("Forenames is required");

            RuleFor(d => d.Surname)
               .NotEmpty()
               .MaximumLength(100)
               .WithMessage("Surname is required");

            RuleFor(t => t.DateOfBirth)
                .NotNull()
                .WithMessage("Date Of Birth date is required.");

            RuleFor(d => d.Telephone)
               .NotEmpty()
               .MaximumLength(100)
               .WithMessage("Telephone Number is required");

            RuleFor(d => d.Mobile)
               .NotEmpty()
               .MaximumLength(100)
               .WithMessage("Mobile Number is required");

            RuleFor(d => d.Address)
               .NotEmpty()
               .MaximumLength(100)
               .WithMessage("Address is required");

            RuleFor(d => d.Address_2)
               .NotEmpty()
               .MaximumLength(100)
               .WithMessage("Address_2 is required");

            RuleFor(d => d.Postcode)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Postcode is required");

            RuleFor(d => d.EMail_Home)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("Home email required");

            RuleFor(t => t.StartDate)
                .NotNull()
                .WithMessage("Start Date date is required.");
        }
    }
}
