namespace Employee.Integration.Application.Common.Model
{
    public record PDFExportResponse(byte[] FileContents, string Options, string FileName);
}
