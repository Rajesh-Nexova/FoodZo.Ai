using Serilog;
using ILogger = Serilog.ILogger;

namespace FoodZOAI.UserManagement.CustomMiddleware
{
    public class AppsettingSerilog
    {
        private static readonly ILogger _logger = Log.ForContext("SourceContext", "AppsettingLogger");

        public static void LogInfo(string message)
        {
            _logger.Information(message);
        }

        public static void LogInfo(string message, object data)
        {
            _logger.Information("{Message} {@Data}", message, data);
        }

        public static void LogError(string message, Exception ex)
        {
            _logger.Error(ex, message);
        }

        public static void LogError(Exception ex)
        {
            _logger.Error(ex, "An error occurred");
        }

        public static void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        public static void LogDebug(string message)
        {
            _logger.Debug(message);
        }
    }
}
