using AutoMapper;
using Employee.Integration.Application.UseCases.Employees.Commands.CreateEmployee;
using Employee.Integration.Application.UseCases.Employees.Commands.DeleteEmployee;
using Employee.Integration.Application.UseCases.Employees.Commands.UpdateEmployee;
using Employee.Integration.Application.UseCases.Employees.Response;

namespace Employee.Integration.Application.Common.Mappings
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            CreateMap<CreateEmployeeCommand, Domain.Entities.Employee>().ReverseMap();
            CreateMap<DeleteEmployeeCommand, Domain.Entities.Employee>().ReverseMap();
            CreateMap<UpdateEmployeeCommand, Domain.Entities.Employee>().ReverseMap();
            CreateMap<EmployeeResponse, Domain.Entities.Employee>().ReverseMap();
        }
    }
}
