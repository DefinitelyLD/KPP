using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.WEB.Logger
{
    public class LoggerConfig
    {
        public static void ConfigureLogger(ILoggerFactory loggerFactory, IConfiguration configuration) 
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

            loggerFactory.AddSerilog();
        }
    }
}
