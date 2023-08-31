using System.Data;
using AutoMapper;
using ClosedXML.Excel;
using Employee.Integration.Application.Common.Interfaces;
using Employee.Integration.Application.Common.Model;
using Employee.Integration.Application.UseCases.Employees.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Integration.Employee.UseCases.Employees.Reports
{
    public class GetEmployeesExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }

    public class GetEmployeesExcelHandler : IRequestHandler<GetEmployeesExcel, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesExcelHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetEmployeesExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook workbook = new())
            {
                var orderData = await GetEmployeesAsync(cancellationToken);
                var excelSheet = workbook.AddWorksheet(orderData, "Employees");

                excelSheet.RowHeight = 20;
                excelSheet.Column(1).Width = 18;
                excelSheet.Column(2).Width = 18;
                excelSheet.Column(3).Width = 18;
                excelSheet.Column(4).Width = 18;
                excelSheet.Column(5).Width = 18;
                excelSheet.Column(6).Width = 18;
                excelSheet.Column(7).Width = 18;
                excelSheet.Column(8).Width = 18;
                excelSheet.Column(9).Width = 18;
                excelSheet.Column(10).Width = 18;
                excelSheet.Column(11).Width = 18;
                excelSheet.Column(12).Width = 18;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);

                    return new ExcelReportResponse(memoryStream.ToArray(), "Employee/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
                }
            }
        }

        private async Task<DataTable> GetEmployeesAsync(CancellationToken cancellationToken = default)
        {
            var AllEmployees = await _context.Employees.ToListAsync(cancellationToken);

            DataTable excelDataTable = new()
            {
                TableName = "Empdata"
            };

            excelDataTable.Columns.Add("Id", typeof(int));
            excelDataTable.Columns.Add("Payroll_Number", typeof(string));
            excelDataTable.Columns.Add("Forenames", typeof(string));
            excelDataTable.Columns.Add("Surname", typeof(int));
            excelDataTable.Columns.Add("DateOfBirth", typeof(DateTime));
            excelDataTable.Columns.Add("Telephone", typeof(string));
            excelDataTable.Columns.Add("Mobile", typeof(string));
            excelDataTable.Columns.Add("Address", typeof(string));
            excelDataTable.Columns.Add("Address_2", typeof(string));
            excelDataTable.Columns.Add("Postcode", typeof(string));
            excelDataTable.Columns.Add("EMail_Home", typeof(int));
            excelDataTable.Columns.Add("StartDate", typeof(DateTime));

            var EmployeesList = _mapper.Map<List<EmployeeResponse>>(AllEmployees);

            if (EmployeesList.Count > 0)
            {
                EmployeesList.ForEach(item =>
                {
                    excelDataTable.Rows.Add(item.Id, item.Payroll_Number, item.Forenames, item.Surname, item.DateOfBirth,
                        item.Telephone, item.Mobile, item.Address, item.Address_2,
                        item.Postcode, item.EMail_Home, item.StartDate);
                });
            }

            return excelDataTable;
        }
    }
}
