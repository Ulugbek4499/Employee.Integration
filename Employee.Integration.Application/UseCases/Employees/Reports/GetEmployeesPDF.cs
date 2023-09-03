using AutoMapper;
using Employee.Integration.Application.Common.Interfaces;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MediatR;
using Employee.Integration.Application.Common.Model;

namespace Employee.Integration.Application.UseCases.Employees.Reports;

public record GetEmployeePDF(string FileName) : IRequest<PDFExportResponse>;

public class GetEmployeePDFHandler : IRequestHandler<GetEmployeePDF, PDFExportResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeePDFHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PDFExportResponse> Handle(GetEmployeePDF request, CancellationToken cancellationToken)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Document document = new Document(PageSize.A4.Rotate());
            document.SetMargins(30, 30, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, ms);

            HeaderFooterHelper headerFooter = new HeaderFooterHelper();
            writer.PageEvent = headerFooter;

            document.Open();

            PdfPTable table = new PdfPTable(12);  // Modify this for the number of columns in your Employee class

            table.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }); // Adjust column widths as needed

            table.SpacingBefore = 10;
            table.SpacingAfter = 10;

            PdfPCell headerCell = new PdfPCell(new Phrase("Employees", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
            headerCell.Colspan = 12; // Adjust the colspan for the number of columns
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            table.AddCell(headerCell);
            table.CompleteRow();

            // Add column headers here for the Employee class
            table.AddCell("ID");
            table.AddCell("Payroll Number");
            table.AddCell("Forenames");
            table.AddCell("Surname");
            table.AddCell("Date Of Birth");
            table.AddCell("Telephone");
            table.AddCell("Mobile");
            table.AddCell("Address");
            table.AddCell("Address 2");
            table.AddCell("Postcode");
            table.AddCell("EMail Home");
            table.AddCell("Start Date");
            table.CompleteRow();

            foreach (var employee in _context.Employees)
            {
                // Add data cells for each property of the Employee class
                table.AddCell(employee.Id.ToString());
                table.AddCell(employee.Payroll_Number);
                table.AddCell(employee.Forenames);
                table.AddCell(employee.Surname);
                table.AddCell(employee.DateOfBirth.ToString("yyyy-MM-dd")); // Format date
                table.AddCell(employee.Telephone);
                table.AddCell(employee.Mobile);
                table.AddCell(employee.Address);
                table.AddCell(employee.Address_2);
                table.AddCell(employee.Postcode);
                table.AddCell(employee.EMail_Home);
                table.AddCell(employee.StartDate.ToString("yyyy-MM-dd")); // Format date
                table.CompleteRow();
            }

            document.Add(table);
            document.Close();

            string fileNameWithExtension = request.FileName + ".pdf";

            return await Task.FromResult(new PDFExportResponse(ms.ToArray(), "application/pdf", fileNameWithExtension));
        }
    }
}


public class HeaderFooterHelper : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfPTable footerTable = new PdfPTable(1);
        footerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
        footerTable.DefaultCell.Border = Rectangle.NO_BORDER;
        footerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        footerTable.AddCell(new Phrase($"Date: {DateTime.Now.ToString("yyyy-MM-dd")}", new Font(Font.FontFamily.HELVETICA, 8)));

        footerTable.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
    }
}