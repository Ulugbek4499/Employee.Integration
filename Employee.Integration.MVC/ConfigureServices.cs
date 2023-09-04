using Serilog;
using Serilog.Events;
using Telegram.Bot;
using TelegramSink;

namespace Employee.Integration.MVC
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            SerilogSettings(configuration);

            services.AddSingleton<ITelegramBotClient>(
                new TelegramBotClient(configuration?.GetConnectionString("TelegramToken")));

            services.AddEndpointsApiExplorer();
            services.AddAuthorization();
            services.AddHttpContextAccessor();

            return services;
        }

        public static void SerilogSettings(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .MinimumLevel.Information()
               .WriteTo.Console()
               .Enrich.FromLogContext()
               .Enrich.WithEnvironmentUserName()
               .Enrich.WithMachineName()
               .Enrich.WithClientIp()
               .WriteTo.TeleSink(
                telegramApiKey: configuration.GetConnectionString("TelegramToken"),
                telegramChatId: "-1001856623462",
                minimumLevel: LogEventLevel.Error)
               .CreateLogger();
        }
    }
}
