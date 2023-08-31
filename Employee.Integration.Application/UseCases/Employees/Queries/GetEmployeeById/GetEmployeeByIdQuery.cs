using AutoMapper;
using Employee.Integration.Application.Common.Exceptions;
using Employee.Integration.Application.Common.Interfaces;
using Employee.Integration.Application.UseCases.Employees.Response;
using MediatR;

namespace Employee.Integration.Application.UseCases.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeResponse>;

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponse>
    {
        readonly IApplicationDbContext _dbContext;
        readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<EmployeeResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var Employee = FilterIfEmployeeExsists(request.Id);

            var result = _mapper.Map<EmployeeResponse>(Employee);
            return await Task.FromResult(result);
        }

        private Domain.Entities.Employee FilterIfEmployeeExsists(int id)
            => _dbContext.Employees
                .Find(id)
                     ?? throw new NotFoundException(
                            " There is no Employee with this Id. ");
    }
}
