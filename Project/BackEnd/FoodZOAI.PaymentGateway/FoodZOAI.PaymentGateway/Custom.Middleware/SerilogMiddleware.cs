using System.Diagnostics;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using ILogger = Serilog.ILogger;

namespace FoodZOAI.PaymentGateway.Custom.Middleware
{
    public class SerilogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly DiagnosticContext _diagnosticContext;

        public SerilogMiddleware(
            RequestDelegate next,
            ILogger logger,
            DiagnosticContext diagnosticContext)
        {
            _next = next;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await EnrichFromRequest(context);
                await _next(context);
                sw.Stop();

                var statusCode = context.Response?.StatusCode;
                var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

                var log = level == LogEventLevel.Error ? LogForErrorContext(context) : _logger;
                log.Write(level, MessageTemplate(context),
                    context.Request.Method,
                    context.Request.Path,
                    statusCode,
                    sw.Elapsed.TotalMilliseconds);
            }
            catch (Exception ex) when (LogException(ex, context, sw))
            {
                throw;
            }
        }

        private static string MessageTemplate(HttpContext context)
            => context.Response.StatusCode > 499
                ? "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms with error"
                : "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        private bool LogException(Exception ex, HttpContext context, Stopwatch sw)
        {
            sw.Stop();

            LogForErrorContext(context)
                .Error(ex, MessageTemplate(context),
                    context.Request.Method,
                    context.Request.Path,
                    500,
                    sw.Elapsed.TotalMilliseconds);

            return false;
        }

        private ILogger LogForErrorContext(HttpContext context)
        {
            var request = context.Request;

            var result = _logger
                .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            return result;
        }

        private async Task EnrichFromRequest(HttpContext context)
        {
            var request = context.Request;
            var requestBody = await GetRequestBody(request);

            _diagnosticContext.Set("RequestBody", requestBody);
            _diagnosticContext.Set("QueryString", request.QueryString.Value);
            _diagnosticContext.Set("ContentType", request.ContentType);
            _diagnosticContext.Set("UserAgent", request.Headers["User-Agent"].FirstOrDefault());
            _diagnosticContext.Set("RemoteIpAddress", context.Connection.RemoteIpAddress);
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            return body;
        }
    }

}
