using Serilog;

namespace Employee.Integration.MVC.Services
{
    public class SerilogService
    {
        public static void SerilogSettings(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                             .Enrich.FromLogContext()
                             .ReadFrom.Configuration(configuration)
                             .CreateLogger();
        }
    }
}
