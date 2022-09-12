namespace AQChecker.Services;

public class RequestHelper
{
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    public RequestHelper(IConfiguration config, ILogger logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task<string> RequestBuilder(string uriContent)
    {
        var baseURL = _config["AQBaseURL"];
        var client = new HttpClient();
        var response = new HttpResponseMessage();

        try
        {
            response = await client.GetAsync($"{baseURL}{uriContent}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}