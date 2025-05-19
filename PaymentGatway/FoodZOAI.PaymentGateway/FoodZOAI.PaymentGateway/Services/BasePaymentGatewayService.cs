using FoodZOAI.PaymentGateway.Configuration;
using FoodZOAI.PaymentGateway.Models;
using System.Text.Json;
using System.Text;
using FoodZOAI.PaymentGateway.Security.Services;
using FoodZOAI.PaymentGateway.CustomException;

namespace FoodZOAI.PaymentGateway.Services
{
    public abstract class BasePaymentGatewayService : IPaymentGatewayService
    {
        protected readonly HttpClient _httpClient;
        protected readonly ILogger _logger;
        protected readonly PaymentGatewayOptions _options;
        protected readonly ISecurityService _securityService;

        protected BasePaymentGatewayService(
            HttpClient httpClient,
            ILogger<BasePaymentGatewayService> logger,
            PaymentGatewayOptions options,
            ISecurityService securityService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));

            // Configure HTTP client
            _httpClient.BaseAddress = new Uri(_options.ApiBaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public abstract Task<PaymentResponse> InitiatePaymentAsync(PaymentRequest request);
        public abstract Task<PaymentResponse> CapturePaymentAsync(string paymentId, decimal amount);
        public abstract Task<PaymentResponse> RefundPaymentAsync(string paymentId, decimal amount, string reason = null);
        public abstract Task<PaymentResponse> GetPaymentStatusAsync(string paymentId);
        public abstract Task<bool> ValidateWebhookSignatureAsync(string payload, string signature);
        public abstract WebhookEvent ParseWebhookEvent(string payload);

        protected virtual async Task<T> SendRequestAsync<T>(HttpMethod method, string endpoint, object data = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, endpoint);

                if (data != null)
                {
                    var jsonContent = JsonSerializer.Serialize(data, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    _logger.LogDebug("Request Content: {Content}", jsonContent);
                }

                // Add authentication headers
                AddAuthenticationHeaders(request);

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                _logger.LogDebug("Response Status: {StatusCode}, Content: {Content}",
                    response.StatusCode, responseContent);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error response: {StatusCode} {Content}",
                        response.StatusCode, responseContent);
                    throw new PaymentGatewayException($"API Error: {response.StatusCode}", responseContent);
                }

                return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex) when (ex is not PaymentGatewayException)
            {
                _logger.LogError(ex, "Error sending request to {Endpoint}", endpoint);
                throw new PaymentGatewayException("Error processing payment request", ex.Message, ex);
            }
        }

        protected virtual void AddAuthenticationHeaders(HttpRequestMessage request)
        {
            // To be implemented by specific providers
        }
    }
}
