using Employee.Integration.Application.UseCases.Employees.Commands.CreateEmployee;
using Employee.Integration.Application.UseCases.Employees.Commands.DeleteEmployee;
using Employee.Integration.Application.UseCases.Employees.Commands.UpdateEmployee;
using Employee.Integration.Application.UseCases.Employees.Queries.GetAllEmployees;
using Employee.Integration.Application.UseCases.Employees.Queries.GetEmployeeById;
using Employee.Integration.Application.UseCases.Employees.Reports;
using Employee.Integration.Employee.UseCases.Employees.Reports;
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
        public async ValueTask<IActionResult> CreateEmployeeFromExcel()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateEmployeeFromExcel(IFormFile excelfile)
        {
            var result = await Mediator.Send(new AddEmployeesFromExcel(excelfile));

            return RedirectToAction("GetAllEmployees");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> CreateEmployeeFromCSV()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async ValueTask<IActionResult> CreateEmployeeFromCSV(IFormFile csvfile)
        {
            var result = await Mediator.Send(new AddEmployeesFromCsv(csvfile));

            return RedirectToAction("GetAllEmployees");
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllEmployees()
        {
            var Employees = await Mediator.Send(new GetAllEmployeesQuery());

            return View(Employees);
        }

        [HttpGet("[action]")]
        public async ValueTask<FileResult> GetAllEmployeesExcel(string fileName = "employeesExcel")
        {
            var result = await Mediator.Send(new GetEmployeesExcel { FileName = fileName });

            return File(result.FileContents, result.Option, result.FileName);
        }

        [HttpGet("[action]")]
        public async ValueTask<IActionResult> GetAllEmployeesCsv(string fileName = "employeesCsv")
        {
            var result = await Mediator.Send(new GetEmployeesCsv { FileName = fileName });

            return File(result.FileContents, result.Option, result.FileName);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllEmployeesPDF(string fileName = "employeesPDF")
        {
            var result = await Mediator.Send(new GetEmployeePDF(FileName: fileName));

            return File(result.FileContents, result.Options, result.FileName);
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
        public async ValueTask<IActionResult> ViewEmployee(int id)
        {
            var Employee = await Mediator.Send(new GetEmployeeByIdQuery(id));

            return View("ViewEmployee", Employee);
        }
    }
}
