using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Integration.Application.Common.Model
{
    public record CsvReportResponse(byte[] FileContents, string Option, string FileName);

}
