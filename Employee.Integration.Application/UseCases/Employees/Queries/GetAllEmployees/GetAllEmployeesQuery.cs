using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Employee.Integration.Application.Common.Interfaces;
using Employee.Integration.Application.UseCases.Employees.Response;
using MediatR;

namespace Employee.Integration.Application.UseCases.Employees.Queries.GetAllEmployees
{
    public record GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeResponse>>;

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllEmployeesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<IEnumerable<EmployeeResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Employee> Employees = _context.Employees;

            return Task.FromResult(_mapper.Map<IEnumerable<EmployeeResponse>>(Employees));
        }
    }
}
