using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using Employee.Integration.Application.Common.Exceptions;
using Employee.Integration.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Integration.Application.UseCases.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand:IRequest
    {
        public int Id { get; set; }
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

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateEmployeeCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee employee = await FilterIfEmployeeExists(request.Id);
            _mapper.Map(request, employee);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<Domain.Entities.Employee> FilterIfEmployeeExists(int id)
         => await _context.Employees
                    .FirstOrDefaultAsync(x => x.Id == id)
                     ?? throw new NotFoundException(
                              " there is no employee with this id. ");
    }
}
