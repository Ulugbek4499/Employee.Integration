using AutoMapper;
using Employee.Integration.Application.Common.Interfaces;
using Employee.Integration.Application.UseCases.Employees.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Integration.Application.UseCases.Employees.Queries.GetAllEmployees
{
    public record GetAllEmployeesQuery : IRequest<EmployeeResponse[]>;

    public class GetEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, EmployeeResponse[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeResponse[]> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var Employees = await _context.Employees.ToArrayAsync();

            return _mapper.Map<EmployeeResponse[]>(Employees);
        }
    }
}
