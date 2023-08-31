using Serilog.Events;
using Serilog;
using Telegram.Bot;
using System.Text.Json.Serialization;
using TelegramSink;

namespace Employee.Integration.MVC
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            SerilogSettings(configuration);
            //services.AddHostedService<BotBackgroundService>();
            services.AddSingleton<ITelegramBotClient>(
                new TelegramBotClient(configuration?.GetConnectionString("TelegramToken")));

            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

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
