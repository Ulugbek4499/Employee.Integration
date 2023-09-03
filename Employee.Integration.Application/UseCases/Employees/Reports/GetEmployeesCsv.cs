using System.Globalization;
using AutoMapper;
using Employee.Integration.Application.Common.Interfaces;
using Employee.Integration.Application.Common.Model;
using Employee.Integration.Application.UseCases.Employees.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Integration.Application.UseCases.Employees.Reports
{
    public record GetEmployeesCsv : IRequest<CsvReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetEmployeesCsvHandler : IRequestHandler<GetEmployeesCsv, CsvReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesCsvHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CsvReportResponse> Handle(GetEmployeesCsv request, CancellationToken cancellationToken)
        {
            var orderData = await GetEmployeesAsync(cancellationToken);

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                // Write the CSV header
                var header = "Id,Payroll Number,Forenames,Surname,Date Of Birth,Telephone,Mobile,Address,Address 2,Postcode,EMail Home,StartDate";
                await streamWriter.WriteLineAsync(header);

                foreach (var item in orderData)
                {
                    // Format the date fields to match your desired format
                    var formattedDateOfBirth = item.DateOfBirth.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var formattedStartDate = item.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    var csvLine = $"{item.Id},{item.Payroll_Number},{item.Forenames},{item.Surname},{formattedDateOfBirth},{item.Telephone},{item.Mobile},{item.Address},{item.Address_2},{item.Postcode},{item.EMail_Home},{formattedStartDate}";
                    await streamWriter.WriteLineAsync(csvLine);
                }

                streamWriter.Flush();

                return new CsvReportResponse(memoryStream.ToArray(), "text/csv", $"{request.FileName}.csv");
            }
        }

        private async Task<List<EmployeeResponse>> GetEmployeesAsync(CancellationToken cancellationToken = default)
        {
            var allEmployees = await _context.Employees.ToListAsync(cancellationToken);

            return _mapper.Map<List<EmployeeResponse>>(allEmployees);
        }
    }
}
