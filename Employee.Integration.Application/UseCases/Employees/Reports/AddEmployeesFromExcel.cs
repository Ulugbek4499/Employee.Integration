using AutoMapper;
using ClosedXML.Excel;
using Employee.Integration.Application.Common.Interfaces;
using Employee.Integration.Application.UseCases.Employees.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Employee.Integration.Employee.UseCases.Employees.Reports
{
        public record AddEmployeesFromExcel(IFormFile ExcelFile) : IRequest<List<EmployeeResponse>>;

        public class AddEmployeesFromExcelHandler : IRequestHandler<AddEmployeesFromExcel, List<EmployeeResponse>>
        {

            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public AddEmployeesFromExcelHandler(IApplicationDbContext context, IMapper mapper)
            {

                _context = context;
                _mapper = mapper;
            }

            public async Task<List<EmployeeResponse>> Handle(AddEmployeesFromExcel request, CancellationToken cancellationToken)
            {
                if (request.ExcelFile == null || request.ExcelFile.Length == 0)
                    throw new ArgumentNullException("File", "file is empty or null");

                var file = request.ExcelFile;
                List<Domain.Entities.Employee> result = new();
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms, cancellationToken);
                    using (var wb = new XLWorkbook(ms))
                    {
                        var sheet1 = wb.Worksheet(1);
                        int startRow = 2;
                        for (int row = startRow; row <= sheet1.LastRowUsed().RowNumber(); row++)
                        {
                            var Employee = new Domain.Entities.Employee()
                            {
                                Payroll_Number = sheet1.Cell(row, 1).GetString(),
                                Forenames = sheet1.Cell(row, 2).GetString(),
                                Surname = sheet1.Cell(row, 3).GetString(),
                                DateOfBirth = DateTime.Parse(sheet1.Cell(row, 4).GetString()),
                                Telephone = sheet1.Cell(row, 5).GetString(),
                                Mobile = sheet1.Cell(row, 6).GetString(),
                                Address = sheet1.Cell(row, 7).GetString(),
                                Address_2 = sheet1.Cell(row, 8).GetString(),
                                Postcode = sheet1.Cell(row, 9).GetString(),
                                EMail_Home = sheet1.Cell(row, 10).GetString(),
                                StartDate = DateTime.Parse(sheet1.Cell(row, 11).GetString()),
                            };

                            result.Add(Employee);
                        }
                    }
                }

                await _context.Employees.AddRangeAsync(result);
                await _context.SaveChangesAsync();
                return _mapper.Map<List<EmployeeResponse>>(result);
            }
        }
}
