using AutoMapper;
using Employee.Integration.Application.Common.Exceptions;
using Employee.Integration.Application.Common.Interfaces;
using Employee.Integration.Application.UseCases.Employees.Notifications;
using MediatR;

namespace Employee.Integration.Application.UseCases.Employees.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(int Id) : IRequest;

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteEmployeeCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IMediator mediator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee Employee = FilterIfEmployeeExsists(request.Id);
            _dbContext.Employees.Remove(Employee);
            await _mediator.Publish(notification: new EmployeeDeletedNotification(Employee.Payroll_Number, Employee.Forenames, Employee.Telephone));
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        private Domain.Entities.Employee FilterIfEmployeeExsists(int id)
                => _dbContext.Employees.FirstOrDefault(c => c.Id == id) ??
                throw new NotFoundException("There is no Employee with this id.");
    }
}
