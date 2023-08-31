using Employee.Integration.Application.UseCases.Employees.Commands.CreateEmployee;
using Employee.Integration.Application.UseCases.Employees.Commands.DeleteEmployee;
using Employee.Integration.Application.UseCases.Employees.Commands.UpdateEmployee;
using Employee.Integration.Application.UseCases.Employees.Queries.GetAllEmployees;
using Employee.Integration.Application.UseCases.Employees.Queries.GetEmployeeById;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Integration.MVC.Controllers
{
    public class EmployeeController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateEmployee()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateEmployee([FromForm] CreateEmployeeCommand Employee)
        {
            await Mediator.Send(Employee);

            return RedirectToAction("GetAllEmployees");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> UpdateEmployee(int Id)
        {
            var Employee = await Mediator.Send(new GetEmployeeByIdQuery(Id));

            return View(Employee);
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> UpdateEmployee([FromForm] UpdateEmployeeCommand Employee)
        {
            await Mediator.Send(Employee);
            return RedirectToAction("GetAllEmployees");
        }

        public async ValueTask<IActionResult> DeleteEmployee(int Id)
        {
            await Mediator.Send(new DeleteEmployeeCommand(Id));

            return RedirectToAction("GetAllEmployees");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllEmployees()
        {
            var Employees = await Mediator.Send(new GetAllEmployeesQuery());

            return View(Employees);
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> ViewEmployee(int id)
        {
            var Employee = await Mediator.Send(new GetEmployeeByIdQuery(id));

            return View("ViewEmployee", Employee);
        }
    }
}
