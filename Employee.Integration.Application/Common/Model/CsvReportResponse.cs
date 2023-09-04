namespace Employee.Integration.Application.Common.Model
{
    public record CsvReportResponse(byte[] FileContents, string Option, string FileName);

}
