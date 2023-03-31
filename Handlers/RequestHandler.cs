using System.Text;
using Newtonsoft.Json;

namespace dadataStandartizer.Handlers;

public class RequestHandler
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _config;
    private readonly ILogger<RequestHandler> _logger;

    public RequestHandler(IHttpClientFactory clientFactory, IConfiguration config, ILogger<RequestHandler> logger)
    {
        _clientFactory = clientFactory;
        _config = config;
        _logger = logger;
    }

    public async Task<string> HandleAddress(string address)
    {
        _logger.LogInformation("Handling address: " + address);
        
        var uri = _config.GetSection("DadataURI").Value;
        var keySection = _config.GetSection("ApiKeys");
        var authKey = keySection.GetSection("AuthKey").Value;
        var secretKey = keySection.GetSection("SecretKey").Value;
        
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var body = JsonConvert.SerializeObject(new List<string> { address });
        request.Content = new StringContent(
            body,
            Encoding.UTF8,
            "application/json"
        );
        
        request.Headers.Add("Authorization", "Token " + authKey);
        request.Headers.Add("X-Secret", secretKey);
        
        var httpClient = _clientFactory.CreateClient();
        var r = await httpClient.SendAsync(request);

        var responseBody = await r.Content.ReadAsStringAsync();
        _logger.LogDebug(responseBody);
        return responseBody;
    }
}