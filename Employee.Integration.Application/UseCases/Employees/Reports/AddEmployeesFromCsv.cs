using AutoMapper;
using Employee.Integration.Application.Common.Interfaces;
using Employee.Integration.Application.UseCases.Employees.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;

namespace Employee.Integration.Application.UseCases.Employees.Reports;
public record AddEmployeesFromCsv(IFormFile CsvFile) : IRequest<List<EmployeeResponse>>;

public class AddEmployeesFromCsvHandler : IRequestHandler<AddEmployeesFromCsv, List<EmployeeResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEmployeesFromCsvHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeResponse>> Handle(AddEmployeesFromCsv request, CancellationToken cancellationToken)
    {
        if (request.CsvFile == null || request.CsvFile.Length == 0)
            throw new ArgumentNullException("File", "File is empty or null");

        var file = request.CsvFile;
        List<Domain.Entities.Employee> result = new();

        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms, cancellationToken);
            ms.Seek(0, SeekOrigin.Begin);

            using (TextFieldParser parser = new TextFieldParser(ms))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Skip the header row
                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (fields.Length < 11)
                    {
                        // Handle invalid data row
                        continue;
                    }

                    var employee = new Domain.Entities.Employee()
                    {
                        Payroll_Number = fields[0],
                        Forenames = fields[1],
                        Surname = fields[2],
                        DateOfBirth = DateTime.Parse(fields[3]),
                        Telephone = fields[4],
                        Mobile = fields[5],
                        Address = fields[6],
                        Address_2 = fields[7],
                        Postcode = fields[8],
                        EMail_Home = fields[9],
                        StartDate = DateTime.Parse(fields[10]),
                    };

                    result.Add(employee);
                }
            }
        }

        await _context.Employees.AddRangeAsync(result);
        await _context.SaveChangesAsync();
        return _mapper.Map<List<EmployeeResponse>>(result);
    }
}