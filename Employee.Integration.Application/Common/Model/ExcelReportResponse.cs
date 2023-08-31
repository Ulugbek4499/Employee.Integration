namespace Employee.Integration.Application.Common.Model
{
    public record ExcelReportResponse(byte[] FileContents, string Option, string FileName);
}
