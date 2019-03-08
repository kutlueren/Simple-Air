using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;

namespace SimpleAir.API.Logging
{
    public static class Logger
    {
        private static readonly ILogger _usageLogger;
        private static readonly ILogger _errorLogger;

        static Logger()
        {
            IConfiguration Configuration = AppSettingsProvider.Configuration;

            var elasticUri = Configuration["ElasticConfiguration:Uri"];
            var username = Configuration["ElasticConfiguration:UserName"];
            var password = Configuration["ElasticConfiguration:Password"];

            _usageLogger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Async(a => a.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    ModifyConnectionSettings = cfg => cfg.BasicAuthentication(username, password),
                    AutoRegisterTemplate = true,
                }))
            .CreateLogger();

            _errorLogger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Async(a => a.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    ModifyConnectionSettings = cfg => cfg.BasicAuthentication(username, password),
                    AutoRegisterTemplate = true,
                }))
            .CreateLogger();
        }

        public static void WriteUsage(LogDetail infoToLog)
        {
            _usageLogger.Write(LogEventLevel.Information, "{@fields}", infoToLog);
        }

        public static void WriteError(LogDetail infoToLog)
        {
            if (infoToLog.Exception != null)
            {
                infoToLog.Message = GetMessageFromException(infoToLog.Exception);
            }

            _errorLogger.Write(LogEventLevel.Information, "{@fields}", infoToLog);
        }

        private static string GetMessageFromException(Exception ex)
        {
            if (ex.InnerException != null)
                return GetMessageFromException(ex.InnerException);

            return ex.ToString();
        }
    }
}
