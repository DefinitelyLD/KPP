using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Messenger.WEB.Logger
{
    public class LoggerConfig
    {
        public static void ConfigureLogger(IConfiguration configuration) 
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

            new LoggerFactory().AddSerilog();
        }
    }
}
