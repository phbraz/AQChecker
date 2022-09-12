using AQChecker.DTO;
using AQChecker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace AQChecker.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _config;
    private readonly RequestHelper _reqHelper;
    private readonly IMemoryCache _memoryCache;
    private const string CountriesCache = "countryList";

    public IndexModel(ILogger<IndexModel> logger, IConfiguration config, IMemoryCache memoryCache)
    {
        _logger = logger;
        _config = config;
        _memoryCache = memoryCache;
        _reqHelper = new RequestHelper(_config, _logger);
    }
    

    public void OnGet()
    {
    }

    public List<CountryDto> GetCountries()
    {
        var countryList = new List<CountryDto>();
        if (_memoryCache.TryGetValue(CountriesCache, out countryList ))
        {
            return countryList;

        }
        else
        {
            countryList = CountriesHelper.GetCountriesList();
            _memoryCache.Set(CountriesCache, countryList);

            return countryList;
        }
    }

    public async Task<IActionResult> OnGetWriteResponse(string countryCode, string city, string sortBy)
    {
        var uriRequest = $"latest?limit=100&page=1&offset=0&sort={sortBy}&radius=1000&country={countryCode}&city={city}&order_by=lastUpdated&dumpRaw=false";
        var callResponse = await _reqHelper.RequestBuilder(uriRequest);
        var result = new JsonResult(callResponse);
        

        return result;
    }
}