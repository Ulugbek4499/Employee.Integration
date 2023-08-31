using AutoMapper;
using Employee.Integration.Application.Common.Interfaces;
using MediatR;

namespace Employee.Integration.Application.UseCases.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string Payroll_Number { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Postcode { get; set; }
        public string EMail_Home { get; set; }
        public DateTime StartDate { get; set; }
    }
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateEmployeeCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee employee = _mapper.Map<Domain.Entities.Employee>(request);
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync();

            return employee.Id;
        }
    }
}
